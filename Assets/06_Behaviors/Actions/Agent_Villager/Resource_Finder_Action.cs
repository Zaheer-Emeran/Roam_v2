using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Resource_Finder", story: "Find nearest resource within [Radius] of [Workpoint] for [Working_Station] and store in [Resource_Position]", category: "Action", id: "abbfced33d5ee5da6cbf2503994e26dd")]
public partial class Resource_Finder_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Workpoint;
    [SerializeReference] public BlackboardVariable<GameObject> Working_Station;
    [SerializeReference] public BlackboardVariable<float> Radius = new BlackboardVariable<float>(5f);
    [SerializeReference] public BlackboardVariable<Vector3> Resource_Position;

protected override Status OnStart()
{
    // 1. read the production data from the building the villager works at
    Building building = Working_Station.Value.GetComponent<Building>();
    if (building == null) return Status.Failure;

    Production_Data production = building.Data.Production;
    if (production == null) return Status.Failure;

    // 2. find the nearest object tagged with either produced resource, inside the radius
    Vector3 center = Workpoint.Value.transform.position;
    float best = Radius.Value;
    bool found = false;

    foreach (Resource_Type type in new[] { production.Produce_A, production.Produce_B })
    {
        if (type == Resource_Type.None) continue;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(type.ToString()))
        {
            float distance = Vector3.Distance(center, obj.transform.position);
            if (distance <= best)
            {
                best = distance;
                // BUFFER: stop 1 meter short of the resource, on the villager's side
                Vector3 direction = (GameObject.transform.position - obj.transform.position).normalized;
                Resource_Position.Value = obj.transform.position + direction * 0.25f;
                found = true;
            }
        }
    }

    return found ? Status.Success : Status.Failure;
}
}

