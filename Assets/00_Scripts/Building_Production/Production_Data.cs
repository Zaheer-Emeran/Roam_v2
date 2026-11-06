using UnityEngine;

public enum Resource_Type
{
    None,
    Talons,
    Wood,
    Rock,
    Stone,
    Iron_Ore,
    Coal
}

[CreateAssetMenu(menuName = "Data/Production")]
public class Production_Data : ScriptableObject
{
    [field: SerializeField] public int Workers { get; private set; }
    [field: SerializeField] public bool Show_Workers { get; private set; }

    [field: SerializeField] public Resource_Type Produce_A { get; private set; }
    [field: SerializeField] public int Produce_A_Amount { get; private set; }
    [field: SerializeField] public float Produce_A_Minutes { get; private set; }

    [field: SerializeField] public Resource_Type Produce_B { get; private set; }
    [field: SerializeField] public int Produce_B_Amount { get; private set; }
    [field: SerializeField] public float Produce_B_Minutes { get; private set; }

    public string Production_Compiler()
    {
        string result = "";

        if (Produce_A != Resource_Type.None)
            result += "+ " + Produce_A_Amount + " " + Produce_A + " per " + Produce_A_Minutes + " minutes\n";
        if (Produce_B != Resource_Type.None)
            result += "+ " + Produce_B_Amount + " " + Produce_B + " per " + Produce_B_Minutes + " minutes\n";

        return result;
    }
}