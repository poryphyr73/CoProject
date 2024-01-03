using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupData : ScriptableObject
{
    public int stackSize = 1;
    public string pickupName;
    public Sprite img;

    public PickupData(int _stackSize, string _name, Sprite _img)
    {
        stackSize = _stackSize;
        name = _name;
        img = _img;
    }
}
