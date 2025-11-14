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
    public SpriteRenderer objectSprite; //get image on game object
    Color imageColor; //color of the image

    private Coroutine followCoroutine; //used for shop dragging

    //Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>(); //initialize   
    }//end of Start

    private void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
        objectSprite = GetComponent<SpriteRenderer>(); //initialize
        imageColor = objectSprite.color; //assign color
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
        //position based on mouse positioning calculated below
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z; 
        transform.position = mouseWorldPos + offset;

        //pointer event at current mouse position in coordinates
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition; //current mouse position
        //UI Raycast
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results); //UI Raycast for detection for GraphicRaycaster component

        //change color to let player know about valid positions
        bool isValidPosition = false; //check if position in map is interactable with towers
        foreach (RaycastResult result in results){
            if (result.gameObject.CompareTag("Grid")){ //must get grid and not other elements
                isValidPosition = true; //correct position
                Debug.Log("Correct Position");
                break; //exit loop to give player feedback
            }//end of if
        }//end of foreach
        checkValidPosition(isValidPosition); //indicate to player placement correctness

    }//end of OnDrag

    //what happens at the end of the drag
    public void OnEndDrag(PointerEventData eventData){
        draggedTower = null;

        //return to original position if not valid
        if (transform.parent==startParent || transform.parent==null){
            transform.position = startPosition;
            transform.SetParent(startParent);
        }//end of if

        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f; //no longer transparent
        canvasGroup.blocksRaycasts = true; //don't need this feature anymore
        resetColor(); //reset to original color

        //play sound to indicate tower has been placed aside from visual aids
        if (placement != null)
            audioSource.PlayOneShot(placement, 0.5f);
    }//end of OnEndDrag

    public void checkValidPosition(bool isValid){
        float alpha = .5f; //transparency level

        if (objectSprite == null){
            return; //do nothing if there is no tower image to alter
        }//end of if

        if (isValid){
            objectSprite.color = new Color(0f, 1f, 0f, alpha); //green with 50% transparency
            //objectSprite.color = new Color(0f, 1f, 0f, alpha); 
        }//end of if
        else{
            objectSprite.color = new Color(1f, 0f, 0f, alpha); //red with 50% transparency
            //objectSprite.color = new Color(1f, 0f, 0f, alpha); 
        }//end of else

    }//end of checkValidPosition 

    //set object to original color
    public void resetColor(){
        if (objectSprite != null){
            objectSprite.color = new Color(imageColor.r, imageColor.g, imageColor.b, 1f);
            //objectSprite.color = new Color(imageColor.r, imageColor.g, imageColor.b, 1f);
        }//end of if
    }//end of resetColor


    //function to start dragging from the shop
    public void StartFromShop(){
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

    public void StopFollowingMouse(){
        if (followCoroutine != null)
            StopCoroutine(followCoroutine);

        draggedTower = null;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //resetColor(); //restore color after releasing drag
    }
}//end of TowerDragNDrop class
