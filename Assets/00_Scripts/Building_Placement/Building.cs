using UnityEngine;
using System.Collections.Generic;
public class Building : MonoBehaviour
{
    public string Description => data.Description;

    public int Cost => data.Cost;

    [SerializeField] private float build_Time = 5f;
    [SerializeField] private Material grey_Material;

    private Building_Model model;
    private Building_Data data;

     // TIMER: remembers the real materials so they can be restored later
    private Renderer[] renderers;
    private Material[][] original_Materials;
    public Building_Data Data => data;

    public void Setup(Building_Data data, float rotation)
    {
        this.data = data;
        model = Instantiate(data.Model, transform.position, Quaternion.identity, transform);
        model.Rotate(rotation);

        // TIMER: skip the wait if this building's data asset has Place_Instantly ticked
        if (data.Place_Instantly) return;

        renderers = model.GetComponentsInChildren<Renderer>();
        original_Materials = new Material[renderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            original_Materials[i] = renderers[i].sharedMaterials;

            Material[] grey = new Material[original_Materials[i].Length];
            for (int j = 0; j < grey.Length; j++)
            {
                grey[j] = grey_Material;
            }
            renderers[i].sharedMaterials = grey;
        }

        // model.gameObject.SetActive(false);
        Invoke(nameof(Finish_Building), build_Time);
    }

    // WORKERS: the villagers currently working in this building
    public List<GameObject> Workers = new List<GameObject>();

    // public bool Add_Worker(GameObject villager)
    // {
    //     Production_Data production = data.Production;
    //     if (production == null) return false;                      // not a production building
    //     if (Workers.Count >= production.Max_Workers) return false;     // full
    //     if (Workers.Contains(villager)) return false;              // already working here

    //     Workers.Add(villager);
    //     return true;
    // }

    // public void Remove_Worker(GameObject villager)
    // {
    //     Workers.Remove(villager);
    // }

    // TIMER: runs once when the wait is over
    private void Finish_Building()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterials = original_Materials[i];
        }
    }
}