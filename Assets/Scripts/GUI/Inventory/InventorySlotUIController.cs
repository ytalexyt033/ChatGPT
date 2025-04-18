using UnityEngine;
using UnityEngine.EventSystems;
using Player.InventorySystem;

public class InventorySlotUIController : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    public Image icon;
    public Text amountText;
    
    private InventorySlot slot;

    public void Initialize(InventorySlot slot)
    {
        this.slot = slot;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (slot == null || slot.IsEmpty)
        {
            icon.sprite = null;
            icon.enabled = false;
            amountText.text = "";
        }
        else
        {
            icon.sprite = slot.item.icon;
            icon.enabled = true;
            amountText.text = slot.amount > 1 ? slot.amount.ToString() : "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"Used item: {slot.item.itemName}");
        }
    }
}