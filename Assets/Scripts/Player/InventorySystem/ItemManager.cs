using UnityEngine;

namespace Player.InventorySystem
{
    public class ItemManager : MonoBehaviour
    {
        public ItemDatabase itemDatabase;

        public InventoryItem CreateItem(string itemId)
        {
            InventoryItem template = itemDatabase.GetItem(itemId);
            if (template == null)
            {
                Debug.LogError($"Item {itemId} not found in database!");
                return null;
            }
            return new InventoryItem(template);
        }
    }
}
