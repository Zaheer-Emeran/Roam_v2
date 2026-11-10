using UnityEngine;

public class Enemy_Detection : MonoBehaviour
{
    [SerializeField] private GameObject detection_Radius;
    [SerializeField] private Villager_Data villager;

    private void Start()
    {
        // give the radius object a relay that reports back to this script
        Detection_Relay relay = detection_Radius.AddComponent<Detection_Relay>();
        relay.Owner = this;
    }

    public void Enemy_Entered(Collider other)
        {
            if (!villager.Is_Defending) return;

            if (other.CompareTag("Zombie"))
            {
                Debug.Log("bloop");
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
