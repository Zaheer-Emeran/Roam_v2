using UnityEngine;
using TMPro;

public class City_Resources : MonoBehaviour
{
    public int Talons = 100;
    public int Wood = 50;
    public int Rock = 50;

    [SerializeField] private TMP_Text resources_Text;

    private void Update()
    {
        resources_Text.text = "Talons: " + Talons + "\nWood: " + Wood + "\nRock: " + Rock;
    }

    public void Change_Talons(int amount)
    {
        Talons += amount;
    }

    public void Change_Wood(int amount)
    {
        Wood += amount;
    }

    public void Change_Rock(int amount)
    {
        Rock += amount;
    }
}