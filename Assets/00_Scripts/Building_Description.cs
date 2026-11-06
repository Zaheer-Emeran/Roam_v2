using UnityEngine;
using TMPro;

public class Building_Description : MonoBehaviour
{
    [SerializeField] private TMP_Text description_Text;
    [SerializeField] private TMP_Text building_Manager_Description_Text;

    [SerializeField] private City_Resources city_resources;

    public void Set_Text(string text)
    {
        description_Text.text = text;
        building_Manager_Description_Text.text = text;
    }
}