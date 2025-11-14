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

    private Coroutine followCoroutine; //used for shop dragging

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
        //position based on mouse positioning calculated below
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z; 
        transform.position = mouseWorldPos + offset;
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
    }


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
        //resetColor(); //restore color after releasing drag
    }
}//end of TowerDragNDrop class
