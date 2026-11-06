using UnityEngine;
using UnityEngine.EventSystems;

[DefaultExecutionOrder(-10)]
public class Building_Manager : MonoBehaviour
{
    [SerializeField] private GameObject building_Manager_Object_UI;
    [SerializeField] private Building_Description building_Description;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // Skip the click if a preview exists (you're placing, not selecting)
        if (FindFirstObjectByType<Building_Preview>() != null) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Building building = hit.collider.GetComponentInParent<Building>();
            if (building != null)
            {
                building_Description.Set_Text(building.Description);
                building_Manager_Object_UI.SetActive(true);
            }
        }
    }
}