using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Add_To_City_Resources", story: "Agent adds [Resource_Target] from [Working_Station] to [City_Resources]", category: "Action", id: "43f370f8cc48d1efadf7ab0ce36ecd54")]
public partial class Add_To_City_Resources_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Resource_Target;
    [SerializeReference] public BlackboardVariable<GameObject> Working_Station;
    [SerializeReference] public BlackboardVariable<GameObject> City_Resources;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

