using UnityEngine;
using UnityEngine.UI;
using Player.InventorySystem;

namespace Player.GUI
{
    public class InventorySlotUIController : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _countText;
        
        private InventoryUIController _inventoryUI;
        private int _slotIndex;

        public void Initialize(InventoryUIController ui, int index)
        {
            _inventoryUI = ui;
            _slotIndex = index;
            UpdateSlot(null, 0);
        }

        public void UpdateSlot(InventoryItem item, int count)
        {
            if (item == null)
            {
                _icon.sprite = null;
                _icon.enabled = false;
                _countText.text = "";
            }
            else
            {
                _icon.sprite = item.icon;
                _icon.enabled = true;
                _countText.text = count > 1 ? count.ToString() : "";
            }
        }

        public void OnQuickMoveClicked()
        {
            _inventoryUI.HandleQuickMove(_slotIndex);
        }
    }
}
