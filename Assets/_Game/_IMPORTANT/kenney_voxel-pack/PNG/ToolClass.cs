using System.Collections;
using UnityEngine;
               
[CreateAssetMenu(fileName = "new  Tool Class", menuName = "Item/Tool")]    

public class ToolClass : ItemClass
{
    [Header("Tool")] // This attribute should be applied to the field, not the enum
    public string toolID;

    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return this; }
    public override MiscClass GetMisc() { return null; }
    public override ConsumableClass GetConsumable() { return null; }
}
