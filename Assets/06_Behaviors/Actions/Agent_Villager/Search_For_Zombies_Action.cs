using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Search_For_Zombies", story: "Log zombies within [Radius] of [Agent]", category: "Action", id: "d8edea1c2de853c751ca62f55ce0b6f7")]
public partial class Search_For_Zombies_Action : Action
{
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

   protected override Status OnStart()
    {
        if (Agent.Value == null) return Status.Failure;

        List<GameObject> zombies = new List<GameObject>();

        foreach (Collider col in Physics.OverlapSphere(Agent.Value.transform.position, Radius.Value))
        {
            if (col.CompareTag("Zombie") && !zombies.Contains(col.gameObject))
            {
                zombies.Add(col.gameObject);
            }
        }

        Debug.Log("Zombies in range: " + zombies.Count);
        foreach (GameObject z in zombies)
        {
            Debug.Log("Zombie: " + z.name);
        }

        return Status.Success;
    }
}

