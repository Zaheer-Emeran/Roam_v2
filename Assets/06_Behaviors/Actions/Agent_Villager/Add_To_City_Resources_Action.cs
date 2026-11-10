using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Add_To_City_Resources", story: "Agent adds [Resource_Target] from [Working_Station] to [City_Resources]", category: "Action", id: "43f370f8cc48d1efadf7ab0ce36ecd54")]
public partial class Add_To_City_Resources_Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Resource_Target;
[SerializeReference] public BlackboardVariable<GameObject> Working_Station;
[SerializeReference] public BlackboardVariable<City_Resources> City_Resources;

protected override Status OnStart()
{
    // 1. make sure everything we need exists
    if (Resource_Target.Value == null) return Status.Failure;
    if (Working_Station.Value == null) return Status.Failure;
    if (City_Resources.Value == null) return Status.Failure;

    // 2. get the production data from the building
    Building building = Working_Station.Value.GetComponent<Building>();
    if (building == null) return Status.Failure;

    Production_Data production = building.Data.Production;
    if (production == null) return Status.Failure;

    // 3. turn the tag text into a Resource_Type
    Resource_Type type;
    bool worked = Enum.TryParse(Resource_Target.Value.tag, out type);
    if (worked == false)
    {
        return Status.Failure;
    }

    // 4. find out how much to add (Produce_A or Produce_B)
    int amount = 0;
    if (type == production.Produce_A)
    {
        amount = production.Produce_A_Amount;
    }
    else if (type == production.Produce_B)
    {
        amount = production.Produce_B_Amount;
    }
    else
    {
        return Status.Failure;
    }

    // 5. add it to the city resources
    City_Resources.Value.Add_Resource(type, amount);
    return Status.Success;
}
}

