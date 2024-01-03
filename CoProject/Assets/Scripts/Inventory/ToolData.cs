using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    WEAPON,
    PICKAXE,
    AXE
}
[CreateAssetMenu(fileName = "Item", menuName = "Items/Pickups/Tools/Tool", order = 1)]
public class ToolData : PickupData
{
    public ToolData(int _stackSize, string _name, Sprite _img) : base(_stackSize, _name, _img) { }

    public int baseDamage { get; }
    public int damageRange { get; }
    public int toolDamage { get; }
    public ToolType type { get; }

    public HeadData head;
    public HandleData handle;
    public BindingData binding;
}
