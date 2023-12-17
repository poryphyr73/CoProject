using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventorySettings
{
    public static readonly Vector2Int slotSize = new(95, 95);

    public static readonly float slotScale = 1f;

    public static readonly float rotationAnimationSpeed = 30f;
}
public class Container : MonoBehaviour
{
    public PickupScriptable[] itemsData;
    public ContainerScriptable containerData { get; }
    
    public ContainerDisplay _gridOnMouse { get; set; }

    public ContainerDisplay[] _containersDisplayed { get; private set; }

    public Pickup _selectedPickup { get; private set; }

    public Pickup itemPrefab;

    private void Awake()
    {
        _containersDisplayed = GameObject.FindObjectsOfType<ContainerDisplay>();
    }

    public void SelectPickup(Pickup _pickup)
    {
        ClearItemReferences(_pickup);
        _selectedPickup = _pickup;
        _selectedPickup.rectTransform.SetParent(transform);
        _selectedPickup.rectTransform.SetAsLastSibling();
    }

    private void DeselectItem()
    {
        _selectedPickup = null;
    }

    public void AddItem(PickupScriptable itemData)
    {
        for (int g = 0; g < _containersDisplayed.Length; g++)
        {
            for (int y = 0; y < _containersDisplayed[g].gridSize.y; y++)
            {
                for (int x = 0; x < _containersDisplayed[g].gridSize.x; x++)
                {
                    Vector2Int slotPosition = new Vector2Int(x, y);

                    for (int r = 0; r < 2; r++)
                    {
                        if (!ExistsItem(slotPosition, _containersDisplayed[g], r == 0 ? itemData._width : itemData._height, r == 0 ? itemData._height : itemData._width))
                        {
                            Pickup newItem = Instantiate(itemPrefab);
                            newItem.rectTransform = newItem.GetComponent<RectTransform>();

                            if (r == 1) newItem.Rotate();

                            newItem.rectTransform.SetParent(_containersDisplayed[g].rectTransform);
                            newItem.rectTransform.sizeDelta = new Vector2(
                                itemData._width * InventorySettings.slotSize.x,
                                itemData._height * InventorySettings.slotSize.y
                            );

                            newItem.referencePosition = slotPosition;
                            newItem.currentContainer = this;

                            for (int xx = 0; xx < (r == 0 ? itemData._width : itemData._height); xx++)
                            {
                                for (int yy = 0; yy < (r == 0 ? itemData._height : itemData._width); yy++)
                                {
                                    int slotX = slotPosition.x + xx;
                                    int slotY = slotPosition.y + yy;

                                    _containersDisplayed[g].pickups[slotX, slotY] = newItem;
                                    _containersDisplayed[g].pickups[slotX, slotY]._type = itemData;
                                }
                            }

                            newItem.rectTransform.localPosition = IndexToInventoryPosition(newItem);
                            newItem.currentContainerDisplay = _containersDisplayed[g];
                            return;
                        }
                    }
                }
            }
        }

        Debug.Log("(Inventory) Not enough slots found to add the item!");
    }

    public void RemoveItem(Pickup item)
    {
        if (item != null)
        {
            ClearItemReferences(item);
            Destroy(item.gameObject);
        }
    }

    public void MoveItem(Pickup item, bool deselectItemInEnd = true)
    {
        Vector2Int slotPosition = GetSlotAtMouseCoords();

        if (ReachedBoundary(slotPosition, _gridOnMouse, item.correctSize.x, item.correctSize.y))
        {
            Debug.Log("Bounds");
            return;
        }

        if (ExistsItem(slotPosition, _gridOnMouse, item.correctSize.x, item.correctSize.y))
        {
            Debug.Log("Item");
            return;
        }

        item.referencePosition = slotPosition;
        item.rectTransform.SetParent(_gridOnMouse.rectTransform);

        for (int x = 0; x < item.correctSize.x; x++)
        {
            for (int y = 0; y < item.correctSize.y; y++)
            {
                int slotX = item.referencePosition.x + x;
                int slotY = item.referencePosition.y + y;

                _gridOnMouse.pickups[slotX, slotY] = item;
            }
        }

        item.rectTransform.localPosition = IndexToInventoryPosition(item);
        item.currentContainerDisplay = _gridOnMouse;

        if (deselectItemInEnd)
        {
            DeselectItem();
        }
    }

    public void SwapItem(Pickup overlapItem, Pickup oldSelectedItem)
    {
        if (ReachedBoundary(overlapItem.referencePosition, _gridOnMouse, oldSelectedItem.correctSize.x, oldSelectedItem.correctSize.y))
        {
            return;
        }

        ClearItemReferences(overlapItem);

        if (ExistsItem(overlapItem.referencePosition, _gridOnMouse, oldSelectedItem.correctSize.x, oldSelectedItem.correctSize.y))
        {
            RevertItemReferences(overlapItem);
            return;
        }

        SelectPickup(overlapItem);
        MoveItem(oldSelectedItem, false);
    }

    public void ClearItemReferences(Pickup _pickup)
    {
        for (int x = 0; x < _pickup.correctSize.x; x++)
        {
            for (int y = 0; y < _pickup.correctSize.y; y++)
            {
                int slotX = _pickup.referencePosition.x + x;
                int slotY = _pickup.referencePosition.y + y;

                _pickup.currentContainerDisplay.pickups[slotX, slotY] = null;
            }
        }
    }

    public void RevertItemReferences(Pickup item)
    {
        for (int x = 0; x < item.correctSize.x; x++)
        {
            for (int y = 0; y < item.correctSize.y; y++)
            {
                int slotX = item.referencePosition.x + x;
                int slotY = item.referencePosition.y + y;

                item.currentContainerDisplay.pickups[slotX, slotY] = item;
            }
        }
    }

    public bool ExistsItem(Vector2Int slotPosition, ContainerDisplay grid, int width = 1, int height = 1)
    {
        if (ReachedBoundary(slotPosition, grid, width, height))
        {
            Debug.Log("Bounds2");
            return true;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int slotX = slotPosition.x + x;
                int slotY = slotPosition.y + y;

                if (grid.pickups[slotX, slotY] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool ReachedBoundary(Vector2Int slotPosition, ContainerDisplay gridReference, int width = 1, int height = 1)
    {
        if (slotPosition.x + width > gridReference.gridSize.x || slotPosition.x < 0)
        {
            return true;
        }

        if (slotPosition.y + height > gridReference.gridSize.y || slotPosition.y < 0)
        {
            return true;
        }

        return false;
    }

    public Vector3 IndexToInventoryPosition(Pickup pickup)
    {
        Vector3 inventorizedPosition =
            new()
            {
                x = pickup.referencePosition.x * InventorySettings.slotSize.x
                    + InventorySettings.slotSize.x * pickup.correctSize.x / 2,
                y = -(pickup.referencePosition.y * InventorySettings.slotSize.y
                    + InventorySettings.slotSize.y * pickup.correctSize.y / 2
                )
            };

        return inventorizedPosition;
    }

    public Vector2Int GetSlotAtMouseCoords()
    {
        if (_gridOnMouse == null)
        {
            return Vector2Int.zero;
        }

        Vector2 gridPosition =
            new(
                Input.mousePosition.x - _gridOnMouse.rectTransform.position.x,
                _gridOnMouse.rectTransform.position.y - Input.mousePosition.y
            );

        Vector2Int slotPosition =
            new(
                (int)(gridPosition.x / (InventorySettings.slotSize.x * InventorySettings.slotScale)),
                (int)(gridPosition.y / (InventorySettings.slotSize.y * InventorySettings.slotScale))
            );

        return slotPosition;
    }

    public Pickup GetItemAtMouseCoords()
    {
        Vector2Int slotPosition = GetSlotAtMouseCoords();

        if (!ReachedBoundary(slotPosition, _gridOnMouse))
        {
            return GetItemFromSlotPosition(slotPosition);
        }

        return null;
    }

    public Pickup GetItemFromSlotPosition(Vector2Int slotPosition)
    {
        return _gridOnMouse.pickups[slotPosition.x, slotPosition.y];
    }
}
