using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupStack
{
    public PickupData data;
    public int stackSize;
    public PickupStack(PickupData _data)
    {
        data = _data;
        stackSize = 1;
    }

    public bool HasFreeSpace() { return stackSize < data.stackSize; }

    public void Add() { stackSize++; }

    public void Add(int i) { stackSize += i; }

    public static PickupStack blank()
    {
        return new PickupStack(new PickupData(1, "", null));
    }
}
