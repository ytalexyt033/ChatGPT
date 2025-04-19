using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    [Header("Settings")]
    public int capacity = 15;
    public int hotbarSize = 5;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

    public void RemoveItem(int slot)
    {
        if (slot < 0 || slot >= items.Count) return;
        if (items[slot].currentStack > 1) items[slot].currentStack--;
        else items.RemoveAt(slot);
    }
}