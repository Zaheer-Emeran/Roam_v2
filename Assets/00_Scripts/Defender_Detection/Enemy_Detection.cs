using UnityEngine;
using Unity.Behavior;

public class Enemy_Detection : MonoBehaviour
{
    [SerializeField] private GameObject detection_Radius;
    [SerializeField] private Villager_Data villager;
    [SerializeField] private BehaviorGraphAgent agent;


    private void Start()
    {
        // give the radius object a relay that reports back to this script
        Detection_Relay relay = detection_Radius.AddComponent<Detection_Relay>();
        relay.Owner = this;
    }

    public void Enemy_Entered(Collider other)
    {
        if (villager != null && !villager.Is_Defending) return;

        if (other.CompareTag("Zombie"))
        {
            agent.SetVariableValue("is_In_Combat", true);
            agent.SetVariableValue("Enemy", other.gameObject);
            agent.Restart();
        }

        if (other.CompareTag("Villager"))
        {
            Debug.Log("BLoop");
            agent.SetVariableValue("is_In_Combat", true);
            agent.SetVariableValue("Enemy", other.gameObject);
            agent.Restart();
        }

    }

    public class Detection_Relay : MonoBehaviour
    {
        public Enemy_Detection Owner;

        private void OnTriggerEnter(Collider other)
        {
            Owner.Enemy_Entered(other);
        }
    }
}