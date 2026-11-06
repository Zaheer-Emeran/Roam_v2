using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


[DefaultExecutionOrder(-10)]
public class Building_Manager : MonoBehaviour
{
    [SerializeField] private GameObject building_Manager_Object_UI;
    [SerializeField] private Building_Description building_Description;
    [SerializeField] private TMP_Text production_Text_UI;

    [SerializeField] private Building_Grid grid;
    [SerializeField] private City_Resources city_resources;
    [SerializeField] private GameObject workers_Object;

    private Building selected_Building;

    private void Update()
    {
        // STOP: right click or Esc
        if (remove_Mode && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)))
        {
            remove_Mode = false;
            return;
        }

        // STOP: another button was clicked (the building buttons create a preview)
        if (remove_Mode && FindFirstObjectByType<Building_Preview>() != null)
        {
            remove_Mode = false;
        }

        if (!Input.GetMouseButtonDown(0)) return;

        // Skip the click if a preview exists (you're placing, not selecting)
        if (FindFirstObjectByType<Building_Preview>() != null) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Pathway"))
        {
            // REMOVE: in remove mode, a clicked pathway is deleted
            if (remove_Mode)
            {
                Building pathway = hit.collider.GetComponentInParent<Building>();
                if (pathway != null)
                {
                    grid.Remove_Building(pathway);
                    Destroy(pathway.gameObject);
                }
                // remove_Mode = false;
            }
            return;
        }
        remove_Mode = false;
        // REMOVE: while in remove mode, clicking anything else does nothing
        Building building = hit.collider.GetComponentInParent<Building>();
        if (building != null)
        {
            selected_Building = building;

            Production_Data production = building.Data.Production;
            string production_Text = production != null ? production.Production_Compiler() : "";
            workers_Object.SetActive(production != null && production.Show_Workers);

            production_Text_UI.text = production_Text;
            building_Description.Set_Description(building.Description);
            building_Manager_Object_UI.SetActive(true);
        }

        }
    }

     // REMOVE: true after the remove-pathway button is clicked
        private bool remove_Mode;

        // REMOVE: hook this up to the new button's On Click
        public void Start_Remove_Mode()
        {
            // STOP: clicking the button again turns remove mode off
            if (remove_Mode)
            {
                remove_Mode = false;
                return;
            }

            // cancel any building preview so it doesn't get in the way
            Building_Preview preview = FindFirstObjectByType<Building_Preview>();
            if (preview != null) Destroy(preview.gameObject);

            building_Manager_Object_UI.SetActive(false);
            remove_Mode = true;
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

    //when the button is clicked again, when right click is clicked, when esc is clicked, when a building is clicked, when a other button is clicked, stop the pathway removing
}