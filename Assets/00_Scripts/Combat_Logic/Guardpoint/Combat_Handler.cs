using System.Collections.Generic;
using UnityEngine;

public class Combat_Handler : MonoBehaviour
{
    public List<GameObject> Entered_Zombies = new List<GameObject>();
    public List<GameObject> Defending_Villagers = new List<GameObject>();

    public void Start_Combat()
    {
        Entered_Zombies.RemoveAll(zombie_Object => zombie_Object == null);
        Defending_Villagers.RemoveAll(villager_Object => villager_Object == null);

        if (Entered_Zombies.Count == 0 || Defending_Villagers.Count == 0) return;

        int fight_Count = Mathf.Max(Entered_Zombies.Count, Defending_Villagers.Count);

        for (int fight_Index = 0; fight_Index < fight_Count; fight_Index++)
        {
            int villager_Index = Mathf.Min(fight_Index, Defending_Villagers.Count - 1);
            int zombie_Index = Mathf.Min(fight_Index, Entered_Zombies.Count - 1);

            Debug.Log(Defending_Villagers[villager_Index].name + " Fights: " + Entered_Zombies[zombie_Index].name);
        }
    }

    public List<GameObject> Debug_Agents()
    {
        List<GameObject> all_Agents = new List<GameObject>();

        all_Agents.AddRange(Entered_Zombies);
        all_Agents.AddRange(Defending_Villagers);

        foreach (GameObject agent_Object in all_Agents)
        {
            if (agent_Object != null)
            {
                Debug.Log("Agent: " + agent_Object.name, agent_Object);
            }
        }

        return all_Agents;
    }
}