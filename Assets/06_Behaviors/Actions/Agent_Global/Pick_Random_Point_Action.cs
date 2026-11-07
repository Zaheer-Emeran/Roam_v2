using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

[NodeDescription(name: "Pick_Random_Point", story: "Agent picks a random wander building, then a point within [Radius] of it and stores it in [Wander_Position]", category: "Action", id: "25afa7c183a60238d578826fbeaf84ec")]
public partial class Pick_Random_Point_Action : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Wander_Position;
    [SerializeReference] public BlackboardVariable<float> Radius = new BlackboardVariable<float>(3f);

    protected override Status OnStart()
    {
        // 1. choose a random building that has Can_Passive_Wander ticked
        Building_Grid grid = Object.FindFirstObjectByType<Building_Grid>();
        if (grid == null) { Debug.Log("No grid found"); return Status.Failure; }

        List<Building> buildings = grid.Get_Wander_Buildings();
        if (buildings.Count == 0) { Debug.Log("No wander buildings"); return Status.Failure; }

        Building chosen = buildings[Random.Range(0, buildings.Count)];

        // 2. choose a random point around that building
        Vector2 offset = Random.insideUnitCircle * Radius.Value;
        Wander_Position.Value = chosen.transform.position + new Vector3(offset.x, 0, offset.y);
        
        return Status.Success;
    }
}