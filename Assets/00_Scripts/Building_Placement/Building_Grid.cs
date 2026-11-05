using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Building_Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    private Building_Grid_Cell[,] grid;

    private void Start()
    {
        grid = new Building_Grid_Cell[width,height];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
             for (int y =0; y< grid.GetLength(0); y++)
            {
                grid[x,y] = new();
            }
        }
    }

    public void Set_Building(Building building, List<Vector3> all_Building_Positions)
    {
        foreach (var p in all_Building_Positions)
        {
             (int x, int y) = World_To_Grid_Position(p);
             grid[x,y].Set_Building(building);
        }
    }

    private (int x, int y) World_To_Grid_Position(Vector3 world_position)
    {
        int x = Mathf.FloorToInt((world_position - transform.position).x / Building_System.Cell_Size);
        int y = Mathf.FloorToInt((world_position - transform.position).z / Building_System.Cell_Size);
        return (x,y);
    }

    public bool Can_Build(List<Vector3> all_Building_Positions)
    {
        foreach (var p in all_Building_Positions)
        {
            (int x, int y) = World_To_Grid_Position(p);
            if (x < 0 || x >= width || y < 0 || y>=height) return false;
            if (!grid[x,y].Is_Empty()) return false;
        }
        return true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (Building_System.Cell_Size <= 0 || width <= 0 || height <= 0) return;
        Vector3 origin = transform.position;
        for (int y = 0; y <= height; y++)
        {
            Vector3 start = origin + new Vector3(0, 0.01f, y * Building_System.Cell_Size);
            Vector3 end = origin + new Vector3(width * Building_System.Cell_Size, 0.01f, y * Building_System.Cell_Size);
            Gizmos.DrawLine(start, end);
        }
        for (int x = 0; x <= width; x++)
        {
            Vector3 start = origin + new Vector3(x * Building_System.Cell_Size, 0.01f, 0);
            Vector3 end = origin + new Vector3(x * Building_System.Cell_Size, 0.01f, height * Building_System.Cell_Size);
            Gizmos.DrawLine(start, end);
        }
    }
}

public class Building_Grid_Cell
{
    private Building building;
    public void Set_Building(Building building)
    {
        this.building = building;
    }

    public bool Is_Empty()
    {
        return building == null;
    }
}
