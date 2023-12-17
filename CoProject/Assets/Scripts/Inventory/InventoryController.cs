using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Container inventory { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        inventory = GetComponent<Container>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Check if mouse is inside a any grid.
            if (!inventory.ReachedBoundary(inventory.GetSlotAtMouseCoords(), inventory._gridOnMouse))
            {
                if (inventory._selectedPickup)
                {
                    Debug.Log("Flag2");
                    Pickup oldSelectedItem = inventory._selectedPickup;
                    Pickup overlapItem = inventory.GetItemAtMouseCoords();

                    if (overlapItem != null)
                    {
                        inventory.SwapItem(overlapItem, oldSelectedItem);
                    }
                    else
                    {
                        inventory.MoveItem(oldSelectedItem);
                    }
                }
                else
                {
                    SelectItemWithMouse();
                }
            }
        }

        // Remove an item from the inventory
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RemoveItemWithMouse();
        }

        // Generates a random item in the inventory
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.AddItem(inventory.itemsData[UnityEngine.Random.Range(0, inventory.itemsData.Length)]);
        }

        if (inventory._selectedPickup != null)
        {
            MoveSelectedItemToMouse();

            if (Input.GetKeyDown(KeyCode.R))
            {
                inventory._selectedPickup.Rotate();
            }
        }
    }

    /// <summary>
    /// Select a new item in the inventory.
    /// </summary>
    private void SelectItemWithMouse()
    {
        Pickup item = inventory.GetItemAtMouseCoords();

        if (item != null)
        {
            inventory.SelectPickup(item);
        }
    }

    /// <summary>
    /// Removes the item from the inventory that the mouse is hovering over.
    /// </summary>
    private void RemoveItemWithMouse()
    {
        Pickup item = inventory.GetItemAtMouseCoords();

        if (item != null)
        {
            inventory.RemoveItem(item);
        }
    }

    /// <summary>
    /// Moves the currently selected object to the mouse.
    /// </summary>
    private void MoveSelectedItemToMouse()
    {
        inventory._selectedPickup.rectTransform.position = new Vector3(
                Input.mousePosition.x
                    + ((inventory._selectedPickup.correctSize.x * InventorySettings.slotSize.x) / 2)
                    - InventorySettings.slotSize.x / 2,
                Input.mousePosition.y
                    - ((inventory._selectedPickup.correctSize.y * InventorySettings.slotSize.y) / 2)
                    + InventorySettings.slotSize.y / 2,
                Input.mousePosition.z
            );
    }
}
