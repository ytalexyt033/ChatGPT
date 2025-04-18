using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUIController : MonoBehaviour
{
    public Transform itemsParent;
    InventorySlotUI[] slots;

    void Start()
    {
        Inventory.instance.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
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