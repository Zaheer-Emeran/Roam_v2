using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

[NodeDescription(name: "Pick_Random_Point", story: "Agent picks a random point within [Radius] of a wander building and stores it in [Wander_Position]. If [is_Defender] or [is_Zombie], use [Workpoint]", category: "Action", id: "25afa7c183a60238d578826fbeaf84ec")]
public partial class Pick_Random_Point_Action : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Wander_Position;
    [SerializeReference] public BlackboardVariable<float> Radius = new BlackboardVariable<float>(3f);
    [SerializeReference] public BlackboardVariable<GameObject> Workpoint;
    [SerializeReference] public BlackboardVariable<bool> is_Defender;
    [SerializeReference] public BlackboardVariable<bool> is_Zombie;

    protected override Status OnStart()
    {
        Vector3 center;

        if (is_Defender.Value == true || is_Zombie.Value == true)
        {
            // defender / zombie: the workpoint is the center
            if (Workpoint.Value == null) { return Status.Failure; }
            center = Workpoint.Value.transform.position;
        }
        else
        {
            // 1. choose a random building that has Can_Passive_Wander ticked
            Building_Grid grid = Object.FindFirstObjectByType<Building_Grid>();
            if (grid == null) { return Status.Failure; }

            List<Building> buildings = grid.Get_Wander_Buildings();
            if (buildings.Count == 0) { return Status.Failure; }

            Building chosen = buildings[Random.Range(0, buildings.Count)];
            center = chosen.transform.position;
        }

        // 2. choose a random point around the center
        Vector2 offset = Random.insideUnitCircle * Radius.Value;
        Wander_Position.Value = center + new Vector3(offset.x, 0, offset.y);

        return Status.Success;
    }
}