using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryTile;
    public RectTransform InventoryRoot;
    public PlayerNetwork LocalPlayer;
    public GameObject[] InventoryTiles;

    public void Start()
    {
        InventoryTiles = new GameObject[InventorySettings.hotbarLength];
        var tileSize = InventoryTile.GetComponent<RectTransform>().
        var offset = (InventorySettings.hotbarLength - 1) * InventorySettings.scale * -55;
        for (int i = 0; i < InventorySettings.hotbarLength; i++)
        {
            InventoryTiles[i] = Instantiate(InventoryTile, InventoryRoot.transform);
            InventoryTiles[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 110 + offset, 0);
        }
            

    }
}
