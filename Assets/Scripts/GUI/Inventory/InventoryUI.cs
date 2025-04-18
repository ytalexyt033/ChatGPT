using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    InventorySlotUI[] slots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
        Inventory.instance.onItemChangedCallback += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.instance.items.Count)
                slots[i].AddItem(Inventory.instance.items[i]);
            else
                slots[i].ClearSlot();
        }
    }
}