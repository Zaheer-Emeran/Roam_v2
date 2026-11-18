using System.Collections.Generic;
using UnityEngine;
using Unity.Behavior;

public class Zombie_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Zombie;
    [SerializeField] private Building_Grid grid;
    [SerializeField] private List<GameObject> walkpoints;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameObject new_Zombie = Instantiate(Zombie, transform.position, Quaternion.identity);

            BehaviorGraphAgent agent = new_Zombie.GetComponent<BehaviorGraphAgent>();
            agent.SetVariableValue("Walkpoint_List", walkpoints);
        }
    }
}