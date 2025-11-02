using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour{
    //set size of the grid map, adjustable in inspector
    public int rows = 10; //number of rows on the grid, height, think matrix
    public int cols = 10; //number of columns on the grid, width
    public GameObject cell; //cell where the tower is placed
    public GameObject tower; //tower to be placed
    private GameObject[,] gridMap; //to track placement of towers, default null if cell is free

    // Start is called before the first frame update
    void Start(){
        gridMap = new GameObject[rows, cols];
        //loop through all position on grid map to fill with cells
        for(int i=0; i<cols; i++){
            for(int j=0; j<rows; j++){
                //create cells at the grid map positions
                Vector3 position = new Vector3(i, j, 0);
                //create cell at position
                Instantiate(cell, position, Quaternion.identity); 
            }//end of for loop
        }//end of for loop
    }//end of Start

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0)){ //set to mouse input for now, left click
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = Vector3Int.FloorToInt(mousePosition);
            int x = gridPosition.x; //get x, y coordinates of the grid map
            int y = gridPosition.y;

            //ensure placement is in grid map
            if (x>=0 && x<cols && y>=0 && y<rows){
                if(gridMap[x,y] == null){ //check if cell is open
                    //get world position on the grid to place tower
                    Vector3 placementPosition = new Vector3(x, y, 0);
                    //create new tower at the position
                    GameObject towerToPlace = Instantiate(tower, placementPosition, Quaternion.identity);
                    gridMap[x, y] = tower; //mark in gridMap that cell is now taken
                }//end of if 
            }//end of if
        }//end of if
    }//end of Update
}//end of GridSystem class
