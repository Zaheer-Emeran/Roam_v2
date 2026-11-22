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
    // already fighting: ignore
    if (agent.GetVariable("is_In_Combat", out BlackboardVariable<bool> in_Combat) && in_Combat.Value == true) return;

    if (CompareTag("Zombie") && other.CompareTag("Zombie")) return;
    if (villager != null && !villager.Is_Defending) return;

    if (other.CompareTag("Zombie") || other.CompareTag("Villager"))
    {
        agent.SetVariableValue("is_In_Combat", true);
        agent.Restart();
        // no Enemy here: the Combat_Manager decides the target
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