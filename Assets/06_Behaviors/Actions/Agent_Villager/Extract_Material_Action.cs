using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Extract_Material", story: "Agent uses [Resource_Target]", category: "Action", id: "e6c4f845f9972d2a2520e2df8d5371dd")]
public partial class Extract_Material_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Resource_Target;

    protected override Status OnStart()
    {
        // the tree or rock may already be gone
        if (Resource_Target.Value == null) return Status.Failure;

        Natural_Resource_Data node = Resource_Target.Value.GetComponentInParent<Natural_Resource_Data>();
        if (node == null) { Debug.Log("No Natural_Resource_Data on " + Resource_Target.Value.name); return Status.Failure; }
        // if (node == null) return Status.Failure;

        node.Use();
        return Status.Success;
    }
}

