using UnityEngine;
using Player.InventorySystem;

namespace Player.GUI.Inventory
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private Transform slotsParent;
        [SerializeField] private InventorySlotUI slotPrefab;
        
        private Inventory _inventory;
        private InventorySlotUI[] _slots;

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory;
            CreateSlots();
            UpdateUI();
            _inventory.OnInventoryChanged += UpdateUI;
        }

        private void CreateSlots()
        {
            _slots = new InventorySlotUI[_inventory.Size];
            for (int i = 0; i < _inventory.Size; i++)
            {
                var slot = Instantiate(slotPrefab, slotsParent);
                slot.Initialize(this, i);
                _slots[i] = slot;
            }
        }

        public void UpdateUI()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].UpdateSlot(_inventory.GetItem(i), _inventory.GetCount(i));
            }
        }

        public void HandleQuickMove(int slotIndex)
        {
            // Реализация быстрого перемещения
        }
    }
}
