using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Building_Model : MonoBehaviour
{
    [SerializeField] private Transform wrapper;
    public float Rotation => wrapper.transform.eulerAngles.y;
    private Building_Shape_Unit[] shapeUnits;

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
