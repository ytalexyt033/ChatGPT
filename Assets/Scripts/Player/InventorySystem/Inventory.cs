using System.Collections.Generic;
using UnityEngine;

namespace Player.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public List<InventorySlot> slots = new List<InventorySlot>();
        public int capacity = 20;

        private void Awake()
        {
            Initialize(capacity);
        }

        public void Initialize(int size)
        {
            slots.Clear();
            for (int i = 0; i < size; i++)
            {
                slots.Add(new InventorySlot());
            }
        }

        public bool AddItem(InventoryItem item, int amount = 1)
        {
            // Попробовать добавить в существующие стеки
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty && slot.item.itemID == item.itemID && !slot.IsFull)
                {
                    int canAdd = item.maxStack - slot.amount;
                    int toAdd = Mathf.Min(amount, canAdd);
                    slot.amount += toAdd;
                    amount -= toAdd;
                    
                    if (amount <= 0) return true;
                }
            }

            // Добавить в пустые слоты
            foreach (var slot in slots)
            {
                if (slot.IsEmpty)
                {
                    slot.SetItem(item, Mathf.Min(amount, item.maxStack));
                    amount -= Mathf.Min(amount, item.maxStack);
                    
                    if (amount <= 0) return true;
                }
            }

            return false;
        }
    }
}