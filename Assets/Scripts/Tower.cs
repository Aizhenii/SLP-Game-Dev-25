using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public Vector2Int CurrentGrid; //what the tower currently occupies 
    public Vector2 cellSize = Vector2.One; //how big the cell is in units
    public Vector2 gridOrigin = Vector2.Zero; //the starting point for the grid
     private Camera mainCam; //putting main camera here so unity won't ask for it multiple times
    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start() //Start runs Align To grid 
    {
        AligntoGrid();
    }

    public void AligntoGrid() // snaps the tower into the nearest grid cell and updates the towers current position
    {
        Vector3 world = transform.position; //position of where the tower is at 

        Vector2Int g = WorldToGrid(world); //convert to int

        CurrentGrid = g; //updating current grid

        transform.position = GridToWorld(g); //saving these coordinates


    }

    public void MoveToGrid(Vector2Int targetCell) //move the tower to the specific cell
    {
        CurrentGrid = targetCell; //updating the current coordinates

        transform.position = GridToWorld(targetCell); //convert back to world and move
    }

    public Vector2Int WorldToGrid(Vector3 world) //converting world pos to a int 
    {
        float localX = world.X - gridOrigin.X; //local x coordinate variable
        float localY = world.Y - gridOrigin.Y; // local y coordinate variable

        float gx = localX / cellSize.X; // convert x by dividing by cell size
        float gy = localY / cellSize.Y; // convert y by dividing by cell size

        int ix = MathF.RoundToInt(gx); //rounding x to the nearest integer 
        int iy = MathF.RoundToInt(gy); //rounding y to the nearest integer

        return new Vector2Int(ix, iy); //returning the new int grid coordinates
    }

    public Vector3 GridToWorld(Vector2Int g) //converts int coordinates to world pos
    {
        float wx = gridOrigin.X + g.x * cellSize.X; //multiplying by cell size and grid origin
        float wy = gridOrigin.Y + g.y * cellSize.Y; //multiplying by cell size and grid origin

        return new Vector3(wx, wy, 0f); //returning the new position
    }
}