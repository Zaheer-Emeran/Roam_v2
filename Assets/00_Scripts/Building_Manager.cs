using UnityEngine;
using UnityEngine.EventSystems;

[DefaultExecutionOrder(-10)]
public class Building_Manager : MonoBehaviour
{
    [SerializeField] private GameObject building_Manager_Object_UI;
    [SerializeField] private Building_Description building_Description;
    [SerializeField] private Building_Grid grid;
    [SerializeField] private City_Resources city_resources;

    private Building selected_Building;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // Skip the click if a preview exists (you're placing, not selecting)
        if (FindFirstObjectByType<Building_Preview>() != null) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Pathway")) return;
            Building building = hit.collider.GetComponentInParent<Building>();
            if (building != null)
            {
                selected_Building = building;
                building_Description.Set_Text(building.Description);
                building_Manager_Object_UI.SetActive(true);
            }
        }

        
    }

    public void Delete_Selected()
    {

        Debug.Log("blep");
        if (selected_Building == null) return;

        Building_Data data = selected_Building.Data;
        city_resources.Change_Talons(data.Cost);
        city_resources.Change_Wood(data.Wood);
        city_resources.Change_Rock(data.Rock);

        grid.Remove_Building(selected_Building);
        Destroy(selected_Building.gameObject);
        selected_Building = null;
        building_Manager_Object_UI.SetActive(false);
    }
}