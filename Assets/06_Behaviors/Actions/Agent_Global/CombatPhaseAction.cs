// using System;
// using Unity.Behavior;
// using UnityEngine;
// using Action = Unity.Behavior.Action;
// using Unity.Properties;

// [Serializable, GeneratePropertyBag]
// [NodeDescription(name: "Combat_Phase", story: "[Agent] attacks Enemy, ends is_In_Combat", category: "Action", id: "4042fa7bc94216aa01f55e5b3c4781b2")]
// public partial class CombatPhaseAction : Action
// {
//     [SerializeReference] public BlackboardVariable<GameObject> Agent;
//     [SerializeReference] public BlackboardVariable<bool> is_In_Combat;
//     [SerializeReference] public BlackboardVariable<GameObject> Enemy;

//     private float wait_Time;
//     protected override Status OnStart()
//         {
//             wait_Time = 0;
//             return Try_Attack();
//         }

//     protected override Status OnUpdate()
//     {
//         return Try_Attack();
//     }

//     private Status Try_Attack()
//     {
//         Fighter me = Agent.Value.GetComponent<Fighter>();
//         if (me == null) return Status.Failure;

//         // no target (just killed one, or none assigned yet): wait for the manager
//         if (me.Target == null)
//         {
//             wait_Time += Time.deltaTime;
//             if (wait_Time > 2f)
//             {
//                 is_In_Combat.Value = false;   // nobody left: combat is over
//                 return Status.Failure;
//             }
//             return Status.Running;
//         }

//         Fighter target = me.Target;
//         Enemy.Value = target.gameObject;      // keep Enemy in sync
//         target.Take_Damage(me.Damage);

//         // killed it: wait here for the next target instead of leaving the loop
//         if (target.Health <= 0)
//         {
//             wait_Time = 0;
//             return Status.Running;
//         }

//         return Status.Success;
//     }
// }