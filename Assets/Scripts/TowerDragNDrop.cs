using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerDragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //[SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public static GameObject draggedTower; //tower being dragged
    Vector3 startPosition;
    Transform startParent;

    private Image assignedImage; //to hold sprite on the gameobject 
    Color imageColor; //to store color for later revertion

    private Coroutine followCoroutine; //used for shop dragging

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        assignedImage = GetComponent<Image>(); //access image
        imageColor = assignedImage.color; //get color
    }//end of Awake

    //what will happen when you start to click and start drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag"); //check if it works
        canvasGroup.alpha = .6f; //make tower transparent so you know item has been selected
        //So the ray cast will ignore the item itself.
        canvasGroup.blocksRaycasts = false; //prevent grid from being moved
        startPosition = transform.position; //what is the parent (currently grid 1)
        startParent = transform.parent;
        transform.SetParent(transform.root); //no longer a child of the grid and just in the root 
        draggedTower = gameObject;
    }//end of OnBeginDrag

    //what happens during the drag
    public void OnDrag(PointerEventData eventData)
    {
        //So the tower will move with our mouse (at same speed)  and so it will be consistant if the canvas has a different scale (other then 1);
        rectTransform.anchoredPosition += eventData.delta;///canvas.scaleFactor; //delta = speed of the mouse

        //pointer event at current mouse position in coordinates
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition; //current mouse position
        //UI Raycast
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results); //UI Raycast for detection for GraphicRaycaster component
        bool isValidPosition = false; //check if position in map is interactable with towers

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Grid")) //must get grid and not other elements
            {
                isValidPosition = true; //correct position
                Debug.Log("Correct Position");
                break; //exit loop to give player feedback
            }//end of if
        }//end of foreach

        checkValidPosition(isValidPosition); //indicate to player placement correctness
    }//end of OnDrag

    //what happens at the end of the drag
    public void OnEndDrag(PointerEventData eventData)
    {
        draggedTower = null;

        //moving the object into new grid
        //will go back to last parent if tower is not close enough to another grid
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }//end of if

        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f; //no longer transparent
        canvasGroup.blocksRaycasts = true; //don't need this feature anymore
        resetColor(); //reset color when object is placed
    }//end of OnEndDrag

    //show the player if the position is valid, green = ok and red = bad
    public void checkValidPosition(bool isValid)
    {
        float alpha = .5f; //transparency level

        if (assignedImage == null)
        {
            return; //do nothing if there is no tower image to alter
        }//end of if

        if (isValid)
        {
            assignedImage.color = new Color(0f, 1f, 0f, alpha); //green with 50% transparency
        }//end of if
        else
        {
            assignedImage.color = new Color(1f, 0f, 0f, alpha); //red with 50% transparency
        }//end of else
    }//end of checkValidPosition

    //set object to original color
    public void resetColor()
    {
        if (assignedImage != null)
        {
            assignedImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 1f);
        }//end of if
    }//end of resetColor

    //function to start dragging from the shop
    public void StartFromShop()
    {
        startParent = transform.parent;
        startPosition = transform.position;
        transform.SetParent(transform.root);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        draggedTower = gameObject;

        followCoroutine = StartCoroutine(FollowMouseUntilPlaced());
    }

    private IEnumerator FollowMouseUntilPlaced()
    {
        while (Input.GetMouseButton(0))
        {
            rectTransform.position = Input.mousePosition;
            yield return null;
        }
        StopFollowingMouse();
    }

    public void StopFollowingMouse()
    {
        if (followCoroutine != null)
            StopCoroutine(followCoroutine);

        draggedTower = null;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        resetColor(); //restore color after releasing drag
    }
}//end of TowerDragNDrop class
