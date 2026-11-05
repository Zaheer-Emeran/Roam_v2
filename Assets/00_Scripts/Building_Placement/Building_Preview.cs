using System.Collections.Generic;
using UnityEngine;

public class Building_Preview : MonoBehaviour
{
    public enum Building_Preview_State
    {
        POSITIVE,
        NEGATIVE
    }
    [SerializeField] private Material positive_Material;
    [SerializeField] private Material negative_Material;

    public Building_Preview_State State {get; private set; } = Building_Preview_State.NEGATIVE;
    public Building_Data Data {get; private set;}
    public Building_Model Building_Model {get; private set;}
    private List<Renderer> renderers = new();

    private List<Collider> colliders = new();
    public void Setup(Building_Data data)
{
    Data = data;
    Building_Model = Instantiate(data.Model, transform.position, Quaternion.identity, transform);
    renderers.AddRange(Building_Model.GetComponentsInChildren<Renderer>());
    colliders.AddRange(Building_Model.GetComponentsInChildren<Collider>());
    foreach (var col in colliders)
    {
        col.enabled = false;
    }
    SetPreviewMaterial(State);
}
    public void Change_State(Building_Preview_State new_State)
    {
        if (new_State == State) return;
        State = new_State;
        SetPreviewMaterial(State);
    }

    public void Rotate(int rotation_Step)
    {
        Building_Model.Rotate(rotation_Step);
    }

    private void SetPreviewMaterial(Building_Preview_State newState)
    {
        Material previewMat = newState == Building_Preview_State.POSITIVE ? positive_Material : negative_Material;
        foreach (var rend in renderers)
        {
            Material[] mats = new Material[rend.sharedMaterials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = previewMat;
            }
            rend.materials = mats;
        }
    }

}
