using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler{
    //[SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    public static GameObject draggedTower; //tower being dragged
    Vector3 startPosition;
    Transform startParent;
    private Vector3 offset; //offset from mouse and tower to keep tower attached to mouse
    [SerializeField] private AudioClip placement; //sound for placing a tower down
    private AudioSource audioSource; //to play sound effects
    private List<Transform> gridPositions = new List<Transform>();
    private Color imageColor; //color of the sprite
    [SerializeField] private SpriteRenderer sprite; //the sprite on the game object
    private Color validColor = new Color(0f, 1f, 0f, 0.5f); //green 50% transparency
    private Color invalidColor = new Color(1f, 0f, 0f, 0.5f); //red 50% transparenc

    private Coroutine followCoroutine; //used for shop dragging

    //Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>(); //initialize

        //get all grid positions in the scene
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Grid");
        foreach (var grid in grids){
            gridPositions.Add(grid.transform);
        }//end of foreach

        imageColor = sprite.color; //get original color of sprite
    }//end of Start

    private void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
    }//end of Awake

    //what will happen when you start to click and start drag
    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("OnBeginDrag"); //check if it works

        canvasGroup.alpha = .6f; //make tower transparent so you know item has been selected
        //So the ray cast will ignore the item itself.
        canvasGroup.blocksRaycasts = false; //prevent grid from being moved

        startPosition = transform.position; //what is the parent (currently grid 1)
        startParent = transform.parent;
        transform.SetParent(transform.root); //no longer a child of the grid and just in the root 

        //calculate offset
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        offset = transform.position - mouseWorldPos;

        draggedTower = gameObject;
    }//end of OnBeginDrag

    //what happens during the drag
    public void OnDrag(PointerEventData eventData){
        //track position with movement
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; //adjust game plane
        transform.position = mouseWorldPos + offset; //keep tower attached to mouse

        //Raycast to detect grid under mouse and see if position is valid
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        //check if this is a grid or not
        if (hit.collider != null && hit.collider.CompareTag("Grid")){
            sprite.color = validColor; //green for valid position
        }//end of if
        else{
            sprite.color = invalidColor; //red for invalid position
        }//end of else
    }//end of OnDrag

    //what happens at the end of the drag
    public void OnEndDrag(PointerEventData eventData){
        draggedTower = null;

        //Find the closest grid to the mouse, don't need to be exactly on the grid
        Transform closestGrid = null;
        float closestDistance = Mathf.Infinity; //to compare
        foreach (var grid in gridPositions){  
            float distance = Vector3.Distance(transform.position, grid.position);
            if (distance < closestDistance){
                closestDistance = distance;
                closestGrid = grid;
            }//end of if
        }//end of foreach

        //snap it on if it is the closest position
        float snapDistance = 1.0f;
        if (closestGrid != null && closestDistance <= snapDistance){
            transform.position = closestGrid.position;
            transform.SetParent(closestGrid);

            //play sound to indicate tower has been placed aside from visual aids
            if (placement != null)
                audioSource.PlayOneShot(placement, 0.5f);

            sprite.color = imageColor; //reset to color of the sprite
        }//end of if
        else{
            //return to original position if no grids are close enough
            transform.position = startPosition;
            transform.SetParent(startParent);
            sprite.color = imageColor; //reset to color of the sprite
        }//end of else

        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f; //no longer transparent
        canvasGroup.blocksRaycasts = true; //don't need this feature anymore
    }//end of OnEndDrag

    //function to start dragging from the shop
    public void StartFromShop()
    {
        startParent = transform.parent;
        startPosition = transform.position;
        transform.SetParent(transform.root);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        draggedTower = gameObject;

        //calculate offset
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        offset = transform.position - mouseWorldPos;

        followCoroutine = StartCoroutine(FollowMouseUntilPlaced());
    }//end of StartFromShop


    private IEnumerator FollowMouseUntilPlaced()
    {
        while (Input.GetMouseButton(0))
        {
            /*
            //rectTransform.position = Input.mousePosition;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            yield return null;
            */
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;
            transform.position = mouseWorldPos + offset;
            yield return null;
        }//end of while
        StopFollowingMouse();
    }//end of IEnumerator

    public void StopFollowingMouse()
    {
        if (followCoroutine != null)
            StopCoroutine(followCoroutine);

        draggedTower = null;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //resetColor(); //restore color after releasing drag
    }
}//end of TowerDragNDrop class
