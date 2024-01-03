using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventorySettings
{
    public const int hotbarLength = 9;
    public const float scale = 1;
}

public class Inventory : MonoBehaviour
{
    public PickupStack[] hotbar = new PickupStack[InventorySettings.hotbarLength];
    public PlayerController owner;
    public int selectedIndex;
    public GameObject genericDropPrefab;
    public UIManager display;
    
    public bool button;
    public bool button2;
    public Pickup temp;

    public void Add(Pickup _pickup)
    {
        PickupData _data = _pickup.data;

        foreach (PickupStack _ps in hotbar)
            if (_ps != null && _data.Equals(_ps.data) && _ps.HasFreeSpace()) //Check if a non-full stack of the pickup exists in the hotbar
            { _ps.Add(); Destroy(_pickup.gameObject); return; } //Add to the non-full stack

        for (int i = 0; i < hotbar.Length; i++)
            if(hotbar[i] == null) { hotbar[i] = new PickupStack(_data); Destroy(_pickup.gameObject); return; } //Add a new stack to the hotbar if there is a slot to do so

        //PLAY ERROR MESSAGE
        //The inventory is entirely full at this point
    }

    public void Drop()
    {
        //Generate a new item with set data
        hotbar[selectedIndex] = null;
    }

    public void Scroll(bool forward)
    {
        selectedIndex += (_ = forward ? 1 : -1);
        if(selectedIndex < 0) { selectedIndex = InventorySettings.hotbarLength - 1; return; }
        if (selectedIndex >= InventorySettings.hotbarLength) { selectedIndex = 0; return; }
        display.SetSelected(selectedIndex);
    }

    public void OnValidate()
    {
        if(button)
        { Add(temp); ; button = false; }
        if(button2)
        { foreach (PickupStack _ps in hotbar) Debug.Log(_ps == null ? "BLANK" : _ps.data.name); button2 = false; }
    }
}
