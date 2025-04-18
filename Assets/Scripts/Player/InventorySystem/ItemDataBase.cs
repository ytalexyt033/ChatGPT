using System.Collections.Generic;
using UnityEngine;

namespace Player.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        public InventoryItem GetItem(string itemId)
        {
            return items.Find(item => item.itemID == itemId);
        }
    }
}
