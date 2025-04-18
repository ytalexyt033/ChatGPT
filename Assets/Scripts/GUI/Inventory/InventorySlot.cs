using UnityEngine;

namespace Player.InventorySystem
{
    [System.Serializable]
    public class InventorySlot
    {
        public InventoryItem item;
        public int amount;
        
        public bool IsEmpty => item == null;
        public bool IsFull => !IsEmpty && amount >= item.maxStack;

        public void Clear()
        {
            item = null;
            amount = 0;
        }

        public void SetItem(InventoryItem newItem, int newAmount)
        {
            item = newItem;
            amount = newAmount;
        }
    }
}