using System.Collections.Generic;
using UnityEngine;
using Unity.Behavior;

public class Combat_Handler : MonoBehaviour
{
    public List<GameObject> Entered_Zombies = new List<GameObject>();
    public List<GameObject> Defending_Villagers = new List<GameObject>();

    private float check_Timer;


    private void Update()
    {
        check_Timer -= Time.deltaTime;
        if (check_Timer > 0f) return;
        check_Timer = 0.25f;

        Entered_Zombies.RemoveAll(zombie_Object => zombie_Object == null);
        Defending_Villagers.RemoveAll(villager_Object => villager_Object == null);

        bool someone_Was_Freed = false;

        foreach (GameObject zombie_Object in Entered_Zombies)
        {
            if (Free_If_Enemy_Is_Gone(zombie_Object)) someone_Was_Freed = true;
        }

        foreach (GameObject villager_Object in Defending_Villagers)
        {
            if (Free_If_Enemy_Is_Gone(villager_Object)) someone_Was_Freed = true;
        }

        if (someone_Was_Freed) Start_Combat();
    }

    private bool Free_If_Enemy_Is_Gone(GameObject agent_Object)
    {
        BehaviorGraphAgent behavior_Agent = agent_Object.GetComponent<BehaviorGraphAgent>();
        if (behavior_Agent == null) return false;

        if (!behavior_Agent.GetVariable("is_In_Combat", out BlackboardVariable<bool> in_Combat_Variable)) return false;
        if (!in_Combat_Variable.Value) return false;

        if (!behavior_Agent.GetVariable("Enemy", out BlackboardVariable<GameObject> enemy_Variable)) return false;
        if (enemy_Variable.Value != null) return false;

        agent_Object.GetComponent<Fighter>().Post_Combat_Cleanup();
        return true;
    }
    
    
    
    
    public void Start_Combat()
    {
        Entered_Zombies.RemoveAll(zombie_Object => zombie_Object == null);
        Defending_Villagers.RemoveAll(villager_Object => villager_Object == null);

        if (Entered_Zombies.Count == 0 || Defending_Villagers.Count == 0) return;

        HashSet<GameObject> assigned_Agents = new HashSet<GameObject>();

        int fight_Count = Mathf.Max(Entered_Zombies.Count, Defending_Villagers.Count);

        for (int fight_Index = 0; fight_Index < fight_Count; fight_Index++)
        {
            int villager_Index = Mathf.Min(fight_Index, Defending_Villagers.Count - 1);
            int zombie_Index = Mathf.Min(fight_Index, Entered_Zombies.Count - 1);

            GameObject villager_Object = Defending_Villagers[villager_Index];
            GameObject zombie_Object = Entered_Zombies[zombie_Index];

            // an agent only gets its Enemy the first time it appears
            if (assigned_Agents.Add(villager_Object))
            {
                Assign_Attackers_And_Blockers(villager_Object, zombie_Object);
            }

            if (assigned_Agents.Add(zombie_Object))
            {
                Assign_Attackers_And_Blockers(zombie_Object, villager_Object);
            }

            // Debug.Log(villager_Object.name + " Fights: " + zombie_Object.name);
        }
    }



    private void Assign_Attackers_And_Blockers(GameObject agent_Object, GameObject enemy_Object)
{
    BehaviorGraphAgent behavior_Agent = agent_Object.GetComponent<BehaviorGraphAgent>();
    if (behavior_Agent == null) return;

    // only restart agents that are not already in combat
    bool was_In_Combat = false;
    if (behavior_Agent.GetVariable("is_In_Combat", out BlackboardVariable<bool> in_Combat_Variable))
    {
        was_In_Combat = in_Combat_Variable.Value;
    }

    if (!was_In_Combat)
    {
        behavior_Agent.Restart();

        UnityEngine.AI.NavMeshAgent nav_Agent = agent_Object.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav_Agent != null) nav_Agent.ResetPath();
    }

    // set the variables AFTER the restart, same order as Set_Working
    behavior_Agent.SetVariableValue("is_In_Combat", true);
    behavior_Agent.SetVariableValue("Enemy", enemy_Object);
}

    // public List<GameObject> Debug_Agents()
    // {
    //     List<GameObject> all_Agents = new List<GameObject>();

    //     all_Agents.AddRange(Entered_Zombies);
    //     all_Agents.AddRange(Defending_Villagers);

    //     foreach (GameObject agent_Object in all_Agents)
    //     {
    //         if (agent_Object != null)
    //         {
    //             Debug.Log("Agent: " + agent_Object.name, agent_Object);
    //         }
    //     }

    //     return all_Agents;
    // }
}