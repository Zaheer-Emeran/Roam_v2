using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building")]
public class Building_Data : ScriptableObject
{
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public Building_Model Model { get; private set; }
    [field: SerializeField] public bool Place_Instantly { get; private set; }

}