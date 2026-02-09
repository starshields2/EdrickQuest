using System;
using UnityEngine;

[Serializable]
public struct TooltipInfo
{
    public string keyword;
    [TextArea]
    public string description;

    // Optional style parsed from markdown heading metadata.
    // Example heading: "## Fireball {color:#ff4444 bold}"
    public string color; // e.g. "#ff4444" or "red"
    public bool bold;
    public bool italic;
}