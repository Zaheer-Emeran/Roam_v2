using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Toggle_Radius", story: "[Enable] [Radius]", category: "Action", id: "ae50ffd0f53122ab79e62b7fb124f899")]
public partial class Toggle_Radius_Action : Action
{
    [SerializeReference] public BlackboardVariable<bool> Enable;
    [SerializeReference] public BlackboardVariable<GameObject> Radius;

    protected override Status OnStart()
    {

        if(Enable)
        {
            Radius.Value.SetActive(true);
        }
        else
        {
            Radius.Value.SetActive(false);
        }

        return Status.Running;
    }

    // protected override Status OnUpdate()
    // {
    //     return Status.Success;
    // }

    // protected override void OnEnd()
    // {
    // }
}

