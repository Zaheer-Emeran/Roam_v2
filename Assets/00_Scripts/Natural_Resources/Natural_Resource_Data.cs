
using UnityEngine;
public class Natural_Resource_Data : MonoBehaviour
{
    public int Max_Uses = 10;
    public int Current_Uses;
    [SerializeField] private int uses_Per_Use = 1;

    private void Start()
    {
        Current_Uses = Max_Uses;
    }

    // call this each time a villager gathers from it
   public void Use()
    {
        Current_Uses -= uses_Per_Use;
        if (Current_Uses <= 0) Delete_Object();
    }
    public void Delete_Object()
    {
        Destroy(gameObject);
    }
}