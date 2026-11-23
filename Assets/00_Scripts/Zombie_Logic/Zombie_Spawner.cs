using System.Collections.Generic;
using UnityEngine;
using Unity.Behavior;

public class Zombie_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Zombie;
    [SerializeField] private Building_Grid grid;
    [SerializeField] private List<GameObject> walkpoints;
    [SerializeField] private List<GameObject> spawnpoints;
    private int zombie_Count = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (spawnpoints.Count == 0) return;

            GameObject random_Spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Count)];

            GameObject new_Zombie = Instantiate(Zombie, random_Spawnpoint.transform.position, Quaternion.identity);
            zombie_Count++;
            new_Zombie.name = "Zombie (" + zombie_Count + ")";
            BehaviorGraphAgent agent = new_Zombie.GetComponent<BehaviorGraphAgent>();
            agent.SetVariableValue("Walkpoint_List", walkpoints);
        }
    }
}