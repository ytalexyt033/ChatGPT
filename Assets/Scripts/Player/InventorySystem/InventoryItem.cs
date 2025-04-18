using UnityEngine;

namespace Player.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class InventoryItem : ScriptableObject
    {
        public string itemID;
        public string itemName;
        public Sprite icon;
        public int maxStack = 1;
        public GameObject prefab;
        
        [TextArea] public string description;
    }
}