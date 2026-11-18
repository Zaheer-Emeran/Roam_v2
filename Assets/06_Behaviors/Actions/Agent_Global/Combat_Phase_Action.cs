using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Combat_Phase", story: "Combat phase between Villager [Villager] and Zombie [Zombie] toggling Villager Attacking [is_Villager_Attacking], ends is_In_Combat[is_In_Combat]", category: "Action", id: "4042fa7bc94216aa01f55e5b3c4781b2")]
public partial class Combat_Phase_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Villager;
    [SerializeReference] public BlackboardVariable<GameObject> Zombie;
    [SerializeReference] public BlackboardVariable<bool> is_Villager_Attacking;
    [SerializeReference] public BlackboardVariable<bool> is_In_Combat;

    protected override Status OnStart()
    {
        if (Villager.Value == null) return Status.Failure;
        if (Zombie.Value == null) return Status.Failure;

        bool target_Died;

        if (is_Villager_Attacking.Value == false)
        {
            // villager's turn: the zombie takes damage
            is_Villager_Attacking.Value = true;
            // Debug.Log("villager attacking is true");
            target_Died = Deal_Damage(Zombie.Value, -5);
        }
        else
        {
            // zombie's turn: the villager takes damage
            is_Villager_Attacking.Value = false;
            // Debug.Log("villager attacking is false");
            target_Died = Deal_Damage(Villager.Value, -10);
        }

        if (target_Died)
        {
            is_In_Combat.Value = false;
            return Status.Failure;
        
        }

        return Status.Success;
    }

    private bool Deal_Damage(GameObject target, int amount)
{
    // is the target a villager?
    Villager_Data villager = target.GetComponent<Villager_Data>();
    if (villager != null)
    {
        villager.Health = villager.Health + amount;
        Debug.Log("Villager health: " + villager.Health);

        if (villager.Health <= 0)
        {
            UnityEngine.Object.Destroy(target);
            return true;   // died
        }
        return false;      // still alive
    }

    // is the target a zombie?
    Zombie_Data zombie = target.GetComponent<Zombie_Data>();
    if (zombie != null)
    {
        zombie.Health = zombie.Health + amount;
        Debug.Log("Zombie health: " + zombie.Health);

        if (zombie.Health <= 0)
        {
            UnityEngine.Object.Destroy(target);
            return true;   // died
        }
        return false;      // still alive
    }

    return false;          // neither, so nothing happens
}
}

