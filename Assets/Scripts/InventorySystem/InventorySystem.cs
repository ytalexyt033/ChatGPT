using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }
    public List<Item> items = new List<Item>();
    public int capacity = 15;
    public int hotbarSize = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= capacity) return false;

        if (item.maxStack > 1)
        {
            var stack = items.Find(i => i.id == item.id && i.currentStack < i.maxStack);
            if (stack != null)
            {
                stack.currentStack++;
                return true;
            }
        }

        item.currentStack = 1;
        items.Add(item);
        return true;
    }

    public Item GetHotbarItem(int slot) => (slot >= 0 && slot < hotbarSize && slot < items.Count) ? items[slot] : null;
}