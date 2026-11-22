using System.Collections.Generic;
using UnityEngine;
using Unity.Behavior;
public class Radius_Detector : MonoBehaviour
{
    private Combat_Handler combat_Handler;

    private List<GameObject> Entered_Zombies => combat_Handler.Entered_Zombies;
    private List<GameObject> Defending_Villagers => combat_Handler.Defending_Villagers;

    private void Awake()
    {
        combat_Handler = GetComponent<Combat_Handler>();
    }

    private Building building_Component;



    private void OnTriggerEnter(Collider other_Collider)
    {
        if (!Is_Defence_Building()) return;

        // VILLAGER: only villagers have Villager_Data
        Villager_Data villager_Data = other_Collider.GetComponentInParent<Villager_Data>();
        if (villager_Data != null)
        {
            Try_Add_Defending_Villager(villager_Data.gameObject);
            return;
        }


        if (!other_Collider.CompareTag("Zombie")) return;

        GameObject zombie_Object = other_Collider.gameObject;
        if (!Entered_Zombies.Contains(zombie_Object))
        {
            Entered_Zombies.Add(zombie_Object);
        }

        combat_Handler.Start_Combat();
        // combat_Handler.Debug_Agents();
        // Debug_Entered_Zombies();
    }


    // private void OnTriggerExit(Collider other_Collider)
    // {
    //     if (!Is_Defence_Building()) return;

    //     Villager_Data villager_Data = other_Collider.GetComponentInParent<Villager_Data>();
    //     if (villager_Data != null)
    //     {
    //         Defending_Villagers.Remove(villager_Data.gameObject);
    //         // combat_Handler.Debug_Agents();f
    //         return;
    //     }

    //     if (!other_Collider.CompareTag("Zombie")) return;

    //     Entered_Zombies.Remove(other_Collider.gameObject);

    //     // combat_Handler.Debug_Agents();

    // }


    private void Try_Add_Defending_Villager(GameObject villager_Object)
    {
        BehaviorGraphAgent behavior_Agent = villager_Object.GetComponent<BehaviorGraphAgent>();
        if (behavior_Agent == null) return;

        // is_Defending false: ignore
        if (!behavior_Agent.GetVariable("is_Defending", out BlackboardVariable<bool> is_Defending_Variable)) return;
        if (!is_Defending_Variable.Value) return;

        // Workpoint is not this building: ignore
        if (!behavior_Agent.GetVariable("Workpoint", out BlackboardVariable<GameObject> workpoint_Variable)) return;
        if (workpoint_Variable.Value != building_Component.gameObject) return;

        if (!Defending_Villagers.Contains(villager_Object))
        {
            Defending_Villagers.Add(villager_Object);
        }
        // combat_Handler.Debug_Agents();

    }



    private bool Is_Defence_Building()
    {
        if (building_Component == null)
        {
            building_Component = GetComponentInParent<Building>();
        }
        return building_Component != null
        && (building_Component.Data.is_Defence_Building || building_Component.Is_Defence_Workpoint);
    }

    // private void Debug_Entered_Zombies()
    // {
    //     Debug.Log("Zombies in radius: " + Entered_Zombies.Count, this);

    //     foreach (GameObject zombie_Object in Entered_Zombies)
    //     {
    //         Debug.Log(" - " + zombie_Object.name, zombie_Object);
    //     }
    // }

    // private void Debug_Entered_Villagers()
    // {
    //     Defending_Villagers.RemoveAll(villager_Object => villager_Object == null);

    //     Debug.Log("Defending villagers in radius: " + Defending_Villagers.Count, this);

    //     foreach (GameObject villager_Object in Defending_Villagers)
    //     {
    //         Debug.Log(" - " + villager_Object.name, villager_Object);
    //     }
    // }
}