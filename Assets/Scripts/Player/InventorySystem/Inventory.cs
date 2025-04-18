using System.Collections.Generic;
using UnityEngine;

namespace Player.InventorySystem
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemId;
        public string displayName;
        public Sprite icon;
        public int maxStack = 1;
        public GameObject prefab;

        public InventoryItem(string id, string name, Sprite icon, int maxStack, GameObject prefab)
        {
            this.itemId = id;
            this.displayName = name;
            this.icon = icon;
            this.maxStack = maxStack;
            this.prefab = prefab;
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public InventoryItem item;
        public int amount;
        
        public bool IsEmpty => item == null;
        public bool IsFull => !IsEmpty && amount >= item.maxStack;
        public int RemainingSpace => IsEmpty ? 0 : item.maxStack - amount;
    }

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
            slots = new List<InventorySlot>(size);
            for (int i = 0; i < size; i++)
            {
                slots.Add(new InventorySlot());
            }
        }

        public bool AddItem(InventoryItem item, int amount = 1)
        {
            // Сначала пробуем добавить в существующие стеки
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty && 
                    slot.item.itemId == item.itemId && 
                    !slot.IsFull)
                {
                    int canAdd = slot.RemainingSpace;
                    int addAmount = Mathf.Min(amount, canAdd);
                    slot.amount += addAmount;
                    amount -= addAmount;
                    
                    if (amount <= 0) return true;
                }
            }

            // Затем добавляем в пустые слоты
            foreach (var slot in slots)
            {
                if (slot.IsEmpty)
                {
                    int addAmount = Mathf.Min(amount, item.maxStack);
                    slot.item = item;
                    slot.amount = addAmount;
                    amount -= addAmount;
                    
                    if (amount <= 0) return true;
                }
            }

            return false; // Не все предметы поместились
        }

        public void RemoveItem(int slotIndex, int amount = 1)
        {
            if (slotIndex < 0 || slotIndex >= slots.Count) return;
            
            var slot = slots[slotIndex];
            if (slot.IsEmpty) return;
            
            slot.amount -= amount;
            if (slot.amount <= 0)
            {
                slot.item = null;
                slot.amount = 0;
            }
        }

        public void SwapSlots(int indexA, int indexB)
        {
            if (indexA < 0 || indexB < 0 || 
                indexA >= slots.Count || indexB >= slots.Count) 
                return;
            
            // Меняем слоты местами
            (slots[indexA], slots[indexB]) = (slots[indexB], slots[indexA]);
        }

        public void Clear()
        {
            foreach (var slot in slots)
            {
                slot.item = null;
                slot.amount = 0;
            }
        }
    }
}
