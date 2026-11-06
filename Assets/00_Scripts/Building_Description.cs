using UnityEngine;
using TMPro;

public class Building_Description : MonoBehaviour
{
    [SerializeField] private TMP_Text description_Text;
    [SerializeField] private TMP_Text building_Manager_Description_Text;

    // [SerializeField] private TMP_Text building_Information;
    // [SerializeField] private TMP_Text building_Manager_Information_Text;

    [SerializeField] private City_Resources city_resources;

    public void Set_Description(string text)
    {
        description_Text.text = text;
        building_Manager_Description_Text.text = text;
    }


    // public void Set_Information(string text)
    // {
    //     building_Information.text = text;
    //     building_Manager_Information_Text.text = text;
    // }


}