using UnityEngine;
using UnityEngine.UI;

namespace Player.GUI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text countText;
        
        private InventoryUIController inventoryUI;
        private int slotIndex;

        public void Initialize(InventoryUIController ui, int index)
        {
            inventoryUI = ui;
            slotIndex = index;
            ClearSlot();
        }

        public void UpdateSlot(InventoryItem item, int count)
        {
            if (item == null)
            {
                ClearSlot();
                return;
            }

            icon.sprite = item.icon;
            icon.enabled = true;
            countText.text = count > 1 ? count.ToString() : "";
        }

        private void ClearSlot()
        {
            icon.sprite = null;
            icon.enabled = false;
            countText.text = "";
        }

        public void OnQuickMoveClicked()
        {
            inventoryUI.HandleQuickMove(slotIndex);
        }
    }
}
