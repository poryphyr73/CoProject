using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryTile;
    public RectTransform InventoryRoot;
    public GameObject[] InventoryTiles;

    public void Start()
    {
        InventoryTiles = new GameObject[InventorySettings.hotbarLength];
        var offset = (InventorySettings.hotbarLength - 1) * InventorySettings.scale * -60;
        for (int i = 0; i < InventorySettings.hotbarLength; i++)
        {
            InventoryTiles[i] = Instantiate(InventoryTile, InventoryRoot.transform);
            InventoryTiles[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 120 + offset, 0);
            foreach (Transform child in InventoryTiles[i].transform) child.gameObject.SetActive(false);
        }
        SetSelected(0);
    }

    public void SetSprite(PickupStack _toDisplay, int _index)
    {
        if (_index < 0 || _index >= InventorySettings.hotbarLength) return;
        GameObject tile = InventoryTiles[_index];

        foreach (Transform child in tile.transform) child.gameObject.SetActive(true);

        bool stackable = _toDisplay.data.stackSize > 1;
        tile.GetComponentInChildren<RawImage>().texture = _toDisplay.data.img.texture;

        TextMeshProUGUI textReference = tile.GetComponentInChildren<TextMeshProUGUI>();
        textReference.text = ""+ (stackable ? _toDisplay.stackSize : "");
    }

    public void NullSlot(int _index)
    {
        if (_index < 0 || _index >= InventorySettings.hotbarLength) return;

        GameObject tile = InventoryTiles[_index];
        foreach (Transform child in tile.transform) child.gameObject.SetActive(false);
    }

    public void SetSelected(int _index)
    {
        if (_index < 0 || _index >= InventorySettings.hotbarLength) return;

        for(int i = 0; i < InventoryTiles.Length; i++) InventoryTiles[i].GetComponent<RawImage>().color = new Color(255, 255, 255, (i == _index ? 1 : 0.6f));
    }
}
