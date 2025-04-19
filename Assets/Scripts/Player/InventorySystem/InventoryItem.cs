using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int count;
    
    public string itemID => item.itemID;
    public Sprite icon => item.icon;
    public GameObject worldPrefab => item.worldPrefab;
    public int maxStack => item.maxStack;
    public string itemName => item.itemName;

    public InventoryItem(Item item, int count = 1)
    {
        this.item = item;
        this.count = count;
    }
}