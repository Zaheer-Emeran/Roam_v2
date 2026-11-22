using System.Collections.Generic;
using UnityEngine;
using Unity.Behavior;
public class Villager_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject villager;
    [SerializeField] private GameObject City_Resources;
    [SerializeField] private Building_Grid grid;
    private int villager_Count = 0;
    
   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Instantiate(villager, transform.position, Quaternion.identity);
            GameObject new_Villager = Instantiate(villager, transform.position, Quaternion.identity);
            villager_Count++;
            new_Villager.name = "Villager (" + villager_Count + ")";

            BehaviorGraphAgent agent = new_Villager.GetComponent<BehaviorGraphAgent>();
            agent.SetVariableValue("City_Resources", City_Resources);
        }
    }
}