using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform itemsParent;
    InventorySlotUIController[] slots;

    void Start()
    {
        Inventory.instance.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlotUIController>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.instance.items.Count)
            {
                slots[i].UpdateSlot(
                    Inventory.instance.items[i].item,
                    Inventory.instance.items[i].count
                );
            }
            else
            {
                slots[i].UpdateSlot(null, 0);
            }
        }
    }
}