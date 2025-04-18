using UnityEngine;

namespace Player.InventorySystem
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemID;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public int maxStack = 1;
        public GameObject worldPrefab;
        
        public enum ItemType { Generic, Weapon, Consumable, Quest, Material }
        public ItemType itemType = ItemType.Generic;
        
        [Header("Usage Settings")]
        public bool isConsumable;
        public float healthRestore;
        public float cooldown;

        public InventoryItem(InventoryItem source)
        {
            itemID = source.itemID;
            displayName = source.displayName;
            description = source.description;
            icon = source.icon;
            maxStack = source.maxStack;
            worldPrefab = source.worldPrefab;
            itemType = source.itemType;
            isConsumable = source.isConsumable;
            healthRestore = source.healthRestore;
            cooldown = source.cooldown;
        }

        public virtual bool Use()
        {
            if (!isConsumable) return false;
            Debug.Log($"Used {displayName}. Restored {healthRestore} HP.");
            return true;
        }
    }
}
