using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Items/Pickup", order = 1)]
public class PickupScriptable : ScriptableObject
{

    public int _stackSize;
    public int _width;
    public int _height;
    public string _name;
    public Sprite img;
}
