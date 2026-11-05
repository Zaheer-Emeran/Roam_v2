using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Building_System : MonoBehaviour
{
        public const float Cell_Size = 1f;

        [SerializeField] private Building_Data building_Data1;
        [SerializeField] private Building_Data building_Data2;
        [SerializeField] private Building_Data building_Data3;
        [SerializeField] private Building_Preview preview_Prefab;
        [SerializeField] private Building building_Prefab;
        [SerializeField] private Building_Grid grid;
        private Building_Preview preview;

        private void Update()
        {
            Vector3 mousePos = GetMouseWorldPosition();
            if (preview != null)
            {
                Handle_Preview(mousePos);
            }
            else
            {
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                preview = CreatePreview(building_Data1, mousePos);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                preview = CreatePreview(building_Data2, mousePos);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                preview = CreatePreview(building_Data3, mousePos);
            }
                
            }
        }

        public void Select_Building(Building_Data data)
        {
            if (preview != null) return;
            preview = CreatePreview(data, GetMouseWorldPosition());
        }

private void Handle_Preview(Vector3 mouse_World_Position)
{
    if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
    {
        Destroy(preview.gameObject);
        preview = null;
        return;
    }

    preview.transform.position = mouse_World_Position;
    List<Vector3> build_Positions = preview.Building_Model.Get_All_Building_Positions();

    // PATHWAY: the grid check is stored on its own now, so the pathway check can be added to it separately below
    bool grid_Ok = grid.Can_Build(build_Positions);

    // PATHWAY: snap the preview to the grid BEFORE the pathway check, so the checkers are tested at the final position and not at the raw mouse position
    if (grid_Ok)
    {
        preview.transform.position = Get_Snapped_Center_Position(build_Positions);
    }

    // PATHWAY: placement is only allowed if the cells are free AND at least one checker touches an object tagged "Pathway"
    bool can_Build = grid_Ok && preview.Building_Model.Has_Adjacent_Pathway();

    if (can_Build)
    {
        preview.Change_State(Building_Preview.Building_Preview_State.POSITIVE);
        if (Input.GetMouseButtonDown(0))
        {
            Place_Building(build_Positions);
            return;
        }
    }
    else
    {
        // PATHWAY: the preview turns red when the grid is blocked OR no pathway is adjacent
        preview.Change_State(Building_Preview.Building_Preview_State.NEGATIVE);
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
        preview.Rotate(90);
    }
}

    private void Place_Building(List<Vector3> building_Positions)
    {
        Building building = Instantiate(building_Prefab,preview.transform.position,Quaternion.identity);
        building.Setup(preview.Data, preview.Building_Model.Rotation);
        grid.Set_Building(building, building_Positions);
        Destroy(preview.gameObject);
        preview = null;
    }

    private Vector3 Get_Snapped_Center_Position(List<Vector3> allBuildingPositions)
{
    List<int> xs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.x)).ToList();
    List<int> zs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.z)).ToList();
    float centerX = (xs.Min() + xs.Max()) / 2f + Cell_Size / 2f;
    float centerZ = (zs.Min() + zs.Max()) / 2f + Cell_Size / 2f;
    return new(centerX, 0, centerZ);
}

        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }
            return Vector3.zero;
        }
        private Building_Preview CreatePreview(Building_Data data, Vector3 position)
        {
            Building_Preview building_Preview = Instantiate(preview_Prefab, position, Quaternion.identity);
            building_Preview.Setup(data);
            return building_Preview;   
        }
}
