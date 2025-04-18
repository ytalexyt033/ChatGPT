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
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    
    public void Add(Item item, int count = 1)
    {
        // Логика добавления предмета
        onItemChangedCallback?.Invoke();
    }
    
    public void Remove(Item item, int count = 1)
    {
        // Логика удаления предмета
        onItemChangedCallback?.Invoke();
    }
}

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int count;
    
    public InventoryItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}