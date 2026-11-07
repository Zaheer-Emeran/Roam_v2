using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Pick_Random_Point", story: "Agent picks a random point within [Radius] of [Target] and stores it in [Wander_Position]", category: "Action", id: "25afa7c183a60238d578826fbeaf84ec")]
public partial class Pick_Random_Point_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<Vector3> Wander_Position;
    [SerializeReference] public BlackboardVariable<float> Radius = new BlackboardVariable<float>(3f);

    protected override Status OnStart()
    {
        Vector2 offset = UnityEngine.Random.insideUnitCircle * Radius.Value;
        Wander_Position.Value = Target.Value.transform.position + new Vector3(offset.x, 0, offset.y);
        return Status.Success;
    }
}

