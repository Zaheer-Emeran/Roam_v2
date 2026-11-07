using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Building_Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    private Building_Grid_Cell[,] grid;
    private Material grid_Material;
    private bool grid_Visible = true;


    private void Update()
    {
        // TAB: toggle the grid between white and transparent
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            grid_Visible = !grid_Visible;
            grid_Material.color = grid_Visible ? Color.white : Color.clear;
        }
    }

    private void Start()
    {

        Draw_Grid();
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

    public void Remove_Building(Building building)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y].Clear_If(building);
            }
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

    // DRAW: builds the grid out of real lines so it shows in the Game view and in builds
    private void Draw_Grid()
    {
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = Color.clear;      // start transparent
        grid_Material = mat;
        grid_Visible = false;         // so the first Tab turns it on

        float size = Building_System.Cell_Size;

        for (int y = 0; y <= height; y++)
        {
            Create_Line(new Vector3(0, 0.01f, y * size), new Vector3(width * size, 0.01f, y * size), mat);
        }
        for (int x = 0; x <= width; x++)
        {
            Create_Line(new Vector3(x * size, 0.01f, 0), new Vector3(x * size, 0.01f, height * size), mat);
        }

        // Material mat = new Material(Shader.Find("Sprites/Default"));
        // mat.color = Color.white;
    }

    private void Create_Line(Vector3 start, Vector3 end, Material mat)
    {
        LineRenderer line = new GameObject("Grid_Line").AddComponent<LineRenderer>();
        line.transform.SetParent(transform);
        line.material = mat;
        line.startWidth = 0.03f;
        line.endWidth = 0.03f;
        line.positionCount = 2;
        line.SetPosition(0, transform.position + start);
        line.SetPosition(1, transform.position + end);
    }

    public List<Building> Get_Wander_Buildings()
    {
        List<Building> result = new List<Building>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Building b = grid[x, y].Get_Building();
                if (b != null && b.Data.Can_Passive_Wander && !result.Contains(b))
                {
                    result.Add(b);
                }
            }
        }

        return result;
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

    public void Clear_If(Building b)
    {
        if (building == b) building = null;
    }

    public Building Get_Building()
    {
        return building;
    }
}
