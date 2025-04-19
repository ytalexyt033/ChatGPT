using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;
    public InventoryItem item;

    public void AddItem(InventoryItem newItem)
    {
        item = newItem;
        icon.sprite = item.item.icon;
        icon.enabled = true;
        countText.text = item.count > 1 ? item.count.ToString() : "";
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }
}