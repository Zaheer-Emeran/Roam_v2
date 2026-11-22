using System.Collections.Generic;
using UnityEngine;
using Unity.Behavior;

public class Workpoint_Alert : MonoBehaviour
{
    List<Fighter> zombiesInRadius = new List<Fighter>();
    GameObject workpointObject;

    void Awake()
    {
        Building workpointBuilding = GetComponentInParent<Building>();
        workpointObject = workpointBuilding != null ? workpointBuilding.gameObject : gameObject;
    }

    void OnTriggerEnter(Collider enteringCollider)
    {
        Fighter zombie = enteringCollider.GetComponentInParent<Fighter>();
        if (zombie == null || !zombie.Is_Zombie) return;

        if (!zombiesInRadius.Contains(zombie)) zombiesInRadius.Add(zombie);

        Fighter nearestZombie = Find_Nearest_Zombie();
        if (nearestZombie != null) Send_Villagers_To_Fight(nearestZombie);
    }

    void OnTriggerExit(Collider leavingCollider)
    {
        Fighter zombie = leavingCollider.GetComponentInParent<Fighter>();
        if (zombie != null) zombiesInRadius.Remove(zombie);
    }

    Fighter Find_Nearest_Zombie()
    {
        zombiesInRadius.RemoveAll(zombie => zombie == null);   // dead or destroyed zombies

        Fighter nearestZombie = null;
        float nearestDistanceSquared = float.MaxValue;

        foreach (Fighter zombie in zombiesInRadius)
        {
            float distanceSquared = (zombie.transform.position - transform.position).sqrMagnitude;
            if (distanceSquared < nearestDistanceSquared)
            {
                nearestDistanceSquared = distanceSquared;
                nearestZombie = zombie;
            }
        }

        return nearestZombie;
    }

    void Send_Villagers_To_Fight(Fighter zombie)
{
    BehaviorGraphAgent zombieAgent = zombie.GetComponent<BehaviorGraphAgent>();
    bool zombieHasTarget = zombieAgent == null || Get_Bool_Variable(zombieAgent, "is_In_Combat");

    foreach (Fighter fighter in FindObjectsByType<Fighter>(FindObjectsSortMode.None))
    {
        if (fighter.Is_Zombie) continue;

        BehaviorGraphAgent villagerAgent = fighter.GetComponent<BehaviorGraphAgent>();
        if (villagerAgent == null) continue;
        if (!Get_Bool_Variable(villagerAgent, "is_Defending")) continue;
        if (!Is_Assigned_To_This_Workpoint(villagerAgent)) continue;
        if (Get_Bool_Variable(villagerAgent, "is_In_Combat")) continue;

        Start_Combat(villagerAgent, zombie.gameObject);

        if (!zombieHasTarget)
        {
            Start_Combat(zombieAgent, fighter.gameObject);
            zombieHasTarget = true;
        }
    }
    }

    void Start_Combat(BehaviorGraphAgent agent, GameObject enemy)
    {
        agent.Restart();
        agent.SetVariableValue("Enemy", enemy);
        agent.SetVariableValue("is_In_Combat", true);
    }

    bool Is_Assigned_To_This_Workpoint(BehaviorGraphAgent agent)
    {
        if (!agent.GetVariable("Workpoint", out BlackboardVariable<GameObject> villagerWorkpoint)) return false;
        return villagerWorkpoint.Value == workpointObject;
    }

    bool Get_Bool_Variable(BehaviorGraphAgent agent, string variableName)
    {
        if (!agent.GetVariable(variableName, out BlackboardVariable<bool> variable)) return false;
        return variable.Value;
    }
}