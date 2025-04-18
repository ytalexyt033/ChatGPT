using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform itemsParent;
    public InventorySlotUIController[] slots;

    private Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        
        slots = itemsParent.GetComponentsInChildren<InventorySlotUIController>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].UpdateSlot(inventory.items[i].item, inventory.items[i].count);
            }
            else
            {
                slots[i].UpdateSlot(null, 0);
            }
        }
    }
}