using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerDisplay : MonoBehaviour, IPointerEnterHandler
{
    public Vector2Int gridSize = new(2, 2);

    public RectTransform rectTransform;

    public Pickup[,] pickups { get; set; }

    public Container thisContainer { get; set; }

    

    private void Awake()
    {
        if (rectTransform != null)
        {
            thisContainer = GameObject.FindObjectOfType<Container>();
            InitializeGrid();
        }
        else
        {
            Debug.LogError("(InventoryGrid) RectTransform not found!");
        }
    }

    /// <summary>
    /// Initialize matrices and grid size.
    /// </summary>
    private void InitializeGrid()
    {
        // Set items matrices
        pickups = new Pickup[gridSize.x, gridSize.y];

        // Set grid size
        Vector2 size =
            new(
                gridSize.x * InventorySettings.slotSize.x,
                gridSize.y * InventorySettings.slotSize.y
            );
        rectTransform.sizeDelta = size;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        thisContainer._gridOnMouse = this;
    }
}
