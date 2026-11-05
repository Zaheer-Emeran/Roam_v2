using UnityEngine;

public class Building : MonoBehaviour
{
    public string Description => data.Description;
    public int Cost => data.Cost;

    private Building_Model model;
    private Building_Data data;

    public void Setup(Building_Data data, float rotation)
    {
        this.data = data;
        model = Instantiate(data.Model, transform.position, Quaternion.identity, transform);
        model.Rotate(rotation);
    }
}