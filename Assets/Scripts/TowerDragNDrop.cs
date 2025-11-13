using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler{
    private Camera mainCamera; //get camera
    private CanvasGroup canvasGroup; 
    public static GameObject draggedTower; //tower being dragged
    private Vector3 startPosition; //initial position of tower
    private Transform startParent; //original slot
    private SpriteRenderer towerSprite; //to change the color later on
    private Color originalColor; //original color of the game object

    //starts when game begins
    private void Awake(){
        mainCamera = Camera.main;
        canvasGroup = GetComponent<CanvasGroup>();
        towerSprite = GetComponent<SpriteRenderer>(); //get the sprite
        if (towerSprite != null){ 
            originalColor = towerSprite.color; //get color of the sprite if it exists
        }//end of if
    }//end of Awake

    public void OnBeginDrag(PointerEventData eventData){
        if (canvasGroup != null){
            canvasGroup.alpha = 0.6f; //semi transparent to indicate drag status
            canvasGroup.blocksRaycasts = false; //ignore for taycasts to interact with it
        }//end of if

        startPosition = transform.position; //set new position when moved
        startParent = transform.parent; //set new parent slot when moved 
        draggedTower = gameObject; //reference to what is being moved
        Debug.Log("Drag Begin");
    }//end of OnBeginDrag

    public void OnDrag(PointerEventData eventData){
        //Convert mouse position to world position
        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mouse);

        //Move the tower to follow the mouse
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);

        //Check if posiion is valid using tags and collider
        Collider2D isHit = Physics2D.OverlapPoint(worldPos); //check if tower interacts with collider
        bool isValidPosition = (isHit!=null && isHit.CompareTag("Grid")); //check if this is a grid to place on
        checkValidPosition(isValidPosition);
        Debug.Log("Dragging");
    }//end of OnDrag

    public void OnEndDrag(PointerEventData eventData){
        draggedTower = null; //null when dragging ends because no need for reference now
         
        //reset to original position if no change
        if (transform.parent == startParent){
            transform.position = startPosition; 
        }//end of if

        //reset changes made during dragging
        if (canvasGroup != null){
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }//end of if
        resetColor(); //reset back to normal color
        Debug.Log("End Drag");
    }//end of OnEndDrag

    //tell player if position is valid or not
    public void checkValidPosition(bool isValid){
        float alpha = 0.5f; //transparency level
        if (towerSprite == null){
            return;
        }//end of if
        else if (isValid){
            towerSprite.color = new Color(0f, 1f, 0f, alpha); //green for correct
        }//end of else if
        else{
            towerSprite.color = new Color(1f, 0f, 0f, alpha); //red for incorrect
        }//end of else
    }//end of checkValidPosition

    //set tower back to original sprite and color
    public void resetColor(){
        if (towerSprite != null){
            towerSprite.color = originalColor;
        }//end of if
    }//end of resetColor
}//end of TowerDragNDrop

