using UnityEngine;
using UnityEngine.UI;
using Player.InventorySystem;

public class HotbarSlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _countText;
    [SerializeField] private Image _selectionBorder;

    private HotbarSystem _hotbarSystem;
    private int _slotIndex;

    public void Initialize(HotbarSystem system, int index)
    {
        _hotbarSystem = system;
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

    public void SetSelected(bool selected)
    {
        _selectionBorder.enabled = selected;
    }

    public void OnSlotClicked()
    {
        _hotbarSystem.SelectSlot(_slotIndex);
    }
}
