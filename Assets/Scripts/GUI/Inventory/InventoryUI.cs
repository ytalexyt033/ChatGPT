using UnityEngine;
using Player.InventorySystem;

namespace Player.GUI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform _slotsParent;
        [SerializeField] private InventorySlotUI _slotPrefab;
        [SerializeField] private HotbarSystem _hotbarSystem;

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
                InventorySlotUI slot = Instantiate(_slotPrefab, _slotsParent);
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
            if (_hotbarSystem == null) return;
            
            InventoryItem item = _inventory.GetItem(slotIndex);
            if (item != null)
            {
                _hotbarSystem.AddItem(item, _inventory.GetCount(slotIndex));
                _inventory.RemoveItem(slotIndex, _inventory.GetCount(slotIndex));
            }
        }
    }
}
