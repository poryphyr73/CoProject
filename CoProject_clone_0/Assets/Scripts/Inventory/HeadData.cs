using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Pickups/Tools/Head", order = 2)]
public class HeadData : ToolPartData
{
    public ToolType type;
    public int damage;
}
