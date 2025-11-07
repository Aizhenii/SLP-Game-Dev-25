using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour, IDropHandler{
    public GameObject Tower{
        get{
            //ensures you can't add more than one tower at once, check if grid it full
            if (transform.childCount > 0){
                return transform.GetChild(0).gameObject;
            }//end of if
            return null;
        }//end of get
    }//end of Tower

    public void OnDrop(PointerEventData eventData){
        Debug.Log("OnDrop");
        //can put in item if there is nothing in there
        if (!Tower){
            TowerDragNDrop.draggedTower.transform.SetParent(transform);
            //drops in center of the grid
            TowerDragNDrop.draggedTower.transform.localPosition = new Vector2(0, 0);
        }//end of if
    }//end of OnDrop
}//end of 
