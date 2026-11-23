using UnityEngine;
using Unity.Behavior;
public class Fighter : MonoBehaviour
{
    public int Health = 100;
    public int Damage = 10;
    public bool Is_Working = false;
    public bool Is_Defending = false;
    public bool Is_Zombie = false;   

    [SerializeField] private bool is_Ranged;
    [SerializeField] public GameObject radius;


    public void Deal_Damage(Fighter target_Fighter)
    {
        if (target_Fighter == null) return;

        target_Fighter.Health -= Damage;
        target_Fighter.Dies_To_Combat_Damage();
    }

    public void Dies_To_Combat_Damage()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Post_Combat_Cleanup()
    {
        BehaviorGraphAgent behavior_Agent = GetComponent<BehaviorGraphAgent>();
        if (behavior_Agent == null) return;

        behavior_Agent.SetVariableValue("is_In_Combat", false);
        behavior_Agent.SetVariableValue("Enemy", (GameObject)null);

        UnityEngine.AI.NavMeshAgent nav_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav_Agent != null) nav_Agent.ResetPath();
    }


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