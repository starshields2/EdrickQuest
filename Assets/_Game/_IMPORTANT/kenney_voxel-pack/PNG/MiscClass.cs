using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "new  Misc Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{
    [Header("Misc")]  // This should be applied to a field, not a method or class
    public string miscDescription;  // Example field to show the header in the Inspector.

    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return null; }
    public override MiscClass GetMisc() { return this; }
    public override ConsumableClass GetConsumable() { return null; }
}
