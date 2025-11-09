using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerDragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler{
    //[SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public static GameObject draggedTower; //tower being dragged
    Vector3 startPosition;
    Transform startParent;
    /*
    private Image assignedImage; //to hold sprite on the gameobject 
    Color imageColor; //to store color for later revertion
    */

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //assignedImage = GetComponent<Image>(); //access image
        //imageColor = assignedImage.color; //get color
    }//end of Awake

    //what will happen when you start to click and start drag
    public void OnBeginDrag(PointerEventData eventData){
        //Debug.Log("OnBeginDrag"); //check if it works
        canvasGroup.alpha = .6f; //make tower transparent so you know item has been selected
        //So the ray cast will ignore the item itself.
        canvasGroup.blocksRaycasts = false; //prevent grid from being moved
        startPosition = transform.position; //what is the parent (currently grid 1)
        startParent = transform.parent;
        transform.SetParent(transform.root); //no longer a child of the grid and just in the root 
        draggedTower = gameObject;
    }//end of OnBeginDrag

    //what happens during the drag
    public void OnDrag(PointerEventData eventData){
        //So the tower will move with our mouse (at same speed)  and so it will be consistant if the canvas has a different scale (other then 1);
        rectTransform.anchoredPosition += eventData.delta;///canvas.scaleFactor; //delta = speed of the mouse

        /*
        //see if position is valid using collider interaction
        Vector3 mouse = Input.mousePosition; //get mouse position for checking
        mouse.z = 0f; //2D not 3D
        Vector3 world = Camera.main.ScreenToWorldPoint(mouse);
        world.z = 0f;
        Collider2D isHit = Physics2D.OverlapPoint(world); //for checking if mouse touches grid
        bool isValidPosition = false; //checker initially false
        if (isHit!=null && isHit.CompareTag("Grid")){ //if mouse hits grid, then change to true
            isValidPosition = true;
            Debug.Log("Allowed position");
        }//end of else
        checkValidPosition(isValidPosition); //send feedback to the player
        */
    }//end of OnDrag

    //what happens at the end of the drag
    public void OnEndDrag(PointerEventData eventData){
        draggedTower = null;

        //moving the object into new grid
        //will go back to last parent if tower is not close enough to another grid
        if (transform.parent == startParent || transform.parent == transform.root){
            transform.position = startPosition;
            transform.SetParent(startParent);
        }//end of if

        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f; //no longer transparent
        canvasGroup.blocksRaycasts = true; //don't need this feature anymore
        //resetColor(); //reset color when object is placed
    }//end of OnEndDrag

    /*
    //show the player if the position is valid, green = ok and red = bad
    public void checkValidPosition(bool isValid){
        float alpha = .5f; //transparency level

        if(assignedImage == null){
            return; //do nothing if there is no tower image to alter
        }//end of if

        if (isValid){
            assignedImage.color = new Color(0f, 1f, 0f, alpha); //green with 50% transparency
        }//end of if
        else{
            assignedImage.color = new Color(1f, 0f, 0f, alpha); //red with 50% transparency
        }//end of else
    }//end of checkValidPosition

    public void resetColor(){
        if(assignedImage != null){
            assignedImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 1f);
        }//end of if
    }//end of resetColor
    */
}//end of TowerDragNDrop class
