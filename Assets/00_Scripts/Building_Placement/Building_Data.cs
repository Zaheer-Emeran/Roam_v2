using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building")]
public class Building_Data : ScriptableObject
{
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int Wood { get; private set; }
    [field: SerializeField] public int Rock { get; private set; }
  


    [field: SerializeField] public Building_Model Model { get; private set; }
    [field: SerializeField] public bool Place_Instantly { get; private set; }
    [field: SerializeField] public bool Drag_Place { get; private set; }

    [field: SerializeField] public Production_Data Production { get; private set; }


    public int Base_Workers { get; private set; }
    public int Fire_Workers { get; private set; }
    public int Water_Workers { get; private set; }
    public int Earth_Workers { get; private set; }
    public int Air_Workers { get; private set; }
    public int Avatar_Workers { get; private set; }

    public string Cost_Compiler()
    {
        string result = "";

        if (Cost != 0) result += "Talons: " + Cost + "\n";
        if (Wood != 0) result += "Wood: " + Wood + "\n";
        if (Rock != 0) result += "Rock: " + Rock + "\n";

        return result;
    }

}