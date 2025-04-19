using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
    public int maxStack = 1;
    [HideInInspector] public int currentStack = 1;

    public enum ItemCategory
    {
        Weapon,
        Consumable,
        Material,
        Tool
    }
    public ItemCategory category;

    public virtual void Use()
    {
        Debug.Log($"Using {itemName} (x{currentStack})");
        currentStack--;
    }
}