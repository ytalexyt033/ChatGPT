using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    
    public List<InventoryItem> items = new List<InventoryItem>();
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    void Awake()
    {
        instance = this;
    }
    
    public void Add(Item item, int count = 1)
    {
        items.Add(new InventoryItem(item, count));
        onItemChangedCallback?.Invoke();
    }
}