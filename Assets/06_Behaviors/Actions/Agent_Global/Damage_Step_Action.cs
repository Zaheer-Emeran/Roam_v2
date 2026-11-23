using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Damage_Step", story: "[Self] deals damage to [Enemy]", category: "Action", id: "d4a82e0ff65b20448d0bfd0fe90acaf5")]
public partial class Damage_Step_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;

    protected override Status OnStart()
    {
        if (Self.Value == null || Enemy.Value == null) return Status.Failure;

        Fighter self_Fighter = Self.Value.GetComponent<Fighter>();
        Fighter enemy_Fighter = Enemy.Value.GetComponent<Fighter>();

        if (self_Fighter == null || enemy_Fighter == null) return Status.Failure;

        self_Fighter.Deal_Damage(enemy_Fighter);

        // if (enemy_Fighter.Health <= 0)
        // {
        //     self_Fighter.Post_Combat_Cleanup();
        // }


        return Status.Success;
    }

    
}

