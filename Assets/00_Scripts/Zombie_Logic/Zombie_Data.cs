using UnityEngine;

public class Zombie_Data : MonoBehaviour
{
    public int Health = 100;
    public int Damage = 10;
    [SerializeField] private bool is_Ranged;
    [SerializeField] public GameObject radius;

}
