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
            Instantiate(villager, transform.position, Quaternion.identity);
        }
    }
}