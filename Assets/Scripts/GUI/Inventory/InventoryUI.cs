using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    InventorySlotUI[] slots;

    void Start()
    {
        Inventory.instance.slots = new List<InventorySlot>(capacity);
        slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.instance.slots.Count)
                slots[i].AddItem(Inventory.instance.slots[i].item);
            else
                slots[i].ClearSlot();
        }
    }
}