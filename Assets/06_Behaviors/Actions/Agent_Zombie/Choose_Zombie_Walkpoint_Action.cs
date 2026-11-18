using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Choose_Zombie_Walkpoint", story: "[Zombie] Chooses [Walkpoint] Start from [Walkpoint_List]", category: "Action", id: "d497ca03242f874c980e82417e21561a")]
public partial class Choose_Zombie_Walkpoint_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Zombie;
    [SerializeReference] public BlackboardVariable<GameObject> Walkpoint;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Walkpoint_List;

    protected override Status OnStart()
{
    if (Zombie.Value == null) return Status.Failure;
    if (Walkpoint_List.Value == null) return Status.Failure;
    if (Walkpoint_List.Value.Count == 0) return Status.Failure;

    // 1. find the index of the closest point
    int closest_Index = Find_Closest_Index();
    if (closest_Index == -1) return Status.Failure;

    // 2. decide: true = closest, false = a neighbour
    int chosen_Index = closest_Index;

    if (Path_Decider(50f) == false)
    {
        // pick left (-1) or right (+1) at random
        int step = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        chosen_Index = closest_Index + step;

        // stay inside the list: if we ran off an end, go the other way
        if (chosen_Index < 0) chosen_Index = closest_Index + 1;
        if (chosen_Index >= Walkpoint_List.Value.Count) chosen_Index = closest_Index - 1;

        // a list with one point has no neighbour
        if (chosen_Index < 0 || chosen_Index >= Walkpoint_List.Value.Count) chosen_Index = closest_Index;
    }

    Walkpoint.Value = Walkpoint_List.Value[chosen_Index];
    return Status.Success;
}


    // finds the closest walkpoint and stores it in Walkpoint
   private int Find_Closest_Index()
    {
        int closest_Index = -1;
        float closest_Distance = float.MaxValue;

        for (int i = 0; i < Walkpoint_List.Value.Count; i++)
        {
            GameObject point = Walkpoint_List.Value[i];
            if (point == null) continue;

            float distance = Vector3.Distance(Zombie.Value.transform.position, point.transform.position);
            if (distance < closest_Distance)
            {
                closest_Distance = distance;
                closest_Index = i;
            }
        }

        return closest_Index;
    }

    // returns true "true_Chance" percent of the time, otherwise false
    private bool Path_Decider(float true_Chance)
    {
        float roll = UnityEngine.Random.Range(0f, 100f);

        if (roll < true_Chance)
        {
            return true;
        }
        return false;
    }
}