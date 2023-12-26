using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    NONE,
    FLAME,
    FREEZE,
}
[CreateAssetMenu(fileName = "Item", menuName = "Items/Pickups/Tools/Binding", order = 3)]
public class BindingData : ToolPartData
{
    public float criticalChance;
    public EffectType effect;
}
