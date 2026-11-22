using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int Health = 100;
    public int Damage = 10;
    public bool Is_Working = false;
    public bool Is_Defending = false;
    public bool Is_Zombie = false;   

    [SerializeField] private bool is_Ranged;
    [SerializeField] public GameObject radius;

}

/*
    public bool Is_Zombie;
[SerializeField] private BehaviorGraphAgent agent;
 public Fighter Target;
    public bool In_Combat;
    private void OnEnable()  { Combat_Manager.Fighters.Add(this); }
    private void OnDisable() { Combat_Manager.Fighters.Remove(this); }
    

    public void Take_Damage(int amount)
    {
        Health -= amount;
        Debug.Log(name + " took " + amount + ", health " + Health);
        if (Health <= 0)
        {
            Debug.Log(name + " died");
            Destroy(gameObject);
        }
    }
*/