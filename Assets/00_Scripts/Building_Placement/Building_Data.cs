using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building")]
public class Building_Data : ScriptableObject
{
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int Wood { get; private set; }
    [field: SerializeField] public int Rock { get; private set; }
    [field: SerializeField] public int Base_Workers { get; private set; }
    [field: SerializeField] public int Fire_Workers { get; private set; }
    [field: SerializeField] public int Water_Workers { get; private set; }
    [field: SerializeField] public int Earth_Workers { get; private set; }
    [field: SerializeField] public int Air_Workers { get; private set; }
    [field: SerializeField] public int Avatar_Workers { get; private set; }


    [field: SerializeField] public Building_Model Model { get; private set; }
    [field: SerializeField] public bool Place_Instantly { get; private set; }
    [field: SerializeField] public bool Drag_Place { get; private set; }


}