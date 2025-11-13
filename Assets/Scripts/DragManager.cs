//using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private Tower selectTower = null; //stores the tower we are dragging currently

    private Camera mainCam; //main camera to convert mouse coord into world coord

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update() //updating every frame
    {
        if (Input.GetMouseButtonDown(0)) //if the player presses the left mouse down
        {
            TrySelectTower();
        }

        if (Input.GetMouseButton(0) && selectTower != null) //if the player is holding the left mouse down
        {
            DragSelectTower();
        }

        if (Input.GetMouseButtonUp(0) && selectTower != null) //if the player releases the left mouse
        {
            DropSelectTower();
        }
    }

    private void TrySelectTower() //selecting a tower with left mouse
    {
        Vector3 mousePos = Input.mousePosition; //getting mouse Pos

        mousePos.z = -mainCam.transform.position.z; //convert to world pos
        Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);


        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero); //checking to make sure it is a tower

        if (hit.collider != null)
        {
            Tower tower = hit.collider.GetComponent<Tower>(); //trying to get tower

            if (tower != null) //if the object left mouse button clicked is a tower
            {
                selectTower = tower; //remember the tower
            }
        }
    }

    private void DragSelectTower() //dragging a tower
    {
        Vector3 mousePos = Input.mousePosition; //getting the mouse position

        mousePos.z = mainCam.transform.position.z; //convert to world pos
        Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);

        selectTower.transform.position = new Vector3(worldPos.x, worldPos.y, 0f); //select tower moving to it's new position

    }

    private void DropSelectTower() //dropping it
    {
        selectTower.AligntoGrid(); //alligning it to the grid (Tower.cs)

        selectTower = null; //clearing the tower reference
        // debug 
        Debug.Log("Tower dropped");
    }

    
}