using System.Collections.Generic;
using UnityEngine;
using Unity.Behavior;
public class Villager_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject villager;
    [SerializeField] private Building_Grid grid;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // WANDER: get every placed building with Can_Passive_Wander ticked
            List<Building> wander_Buildings = grid.Get_Wander_Buildings();

            // nothing to wander around, so don't spawn
            if (wander_Buildings.Count == 0) return;

            // pick one at random
            Building chosen = wander_Buildings[Random.Range(0, wander_Buildings.Count)];

            GameObject new_Villager = Instantiate(villager, transform.position, Quaternion.identity);
            BehaviorGraphAgent agent = new_Villager.GetComponent<BehaviorGraphAgent>();
            agent.SetVariableValue("Target", chosen.gameObject);

            // DEBUG: confirm the blackboard received the target
            if (agent.GetVariable("Target", out BlackboardVariable<GameObject> target))
            {
                Debug.Log("Target is " + target.Value.name);
            }
        }
    }
}