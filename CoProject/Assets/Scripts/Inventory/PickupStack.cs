using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupStack
{
    public PickupData data;
    public string name;
    public int stackSize, maxStackSize;
    public Sprite display;
    public PickupStack(PickupData _data)
    {
        data = _data;
        name = data.name;
        maxStackSize = data._stackSize;
        stackSize = 1;
        display = data.img;
    }

    public bool HasFreeSpace() { return stackSize < maxStackSize; }

    public void Add() { stackSize++; }

    public void Add(int i) { stackSize += i; }
}
