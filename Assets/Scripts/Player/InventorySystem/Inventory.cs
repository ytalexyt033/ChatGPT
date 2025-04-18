using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public int capacity = 20;
    public List<InventoryItem> items = new List<InventoryItem>();
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool AddItem(Item item, int count = 1)
    {
        // Логика добавления предмета
        onItemChangedCallback?.Invoke();
        return true;
    }
}