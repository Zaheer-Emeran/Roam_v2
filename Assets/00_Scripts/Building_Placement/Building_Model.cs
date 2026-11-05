using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Building_Model : MonoBehaviour
{
    [SerializeField] private Transform wrapper;
    public float Rotation => wrapper.transform.eulerAngles.y;
    private Building_Shape_Unit[] shapeUnits;


    //Pathway Checkers
    [SerializeField] private Transform[] pathway_Checkers;
    [SerializeField] private bool requires_Pathway = true;

    public bool Has_Adjacent_Pathway()
{
    if (!requires_Pathway) return true;

    foreach (var checker in pathway_Checkers)
    {
        foreach (var col in Physics.OverlapSphere(checker.position, 0.1f))
        {
            if (col.CompareTag("Pathway"))
            {
                Debug.Log("Pathway found next to " + name);
                return true;
            }
        }
    }
    Debug.Log("No pathway next to " + name);
    return false;
}

    private void OnDrawGizmosSelected()
{
    if (pathway_Checkers == null) return;
    Gizmos.color = Color.yellow;
    foreach (var checker in pathway_Checkers)
    {
        if (checker != null) Gizmos.DrawWireSphere(checker.position, 0.1f);
    }
}
    //________________________________




    private void Awake()
    {
        shapeUnits = GetComponentsInChildren<Building_Shape_Unit>();

    }

    public void Rotate(float rotation_Step)
    {
        wrapper.Rotate(0, rotation_Step, 0);
    }

    public List<Vector3> Get_All_Building_Positions()
    {
        return shapeUnits.Select(unit => unit.transform.position).ToList();
    }

}
