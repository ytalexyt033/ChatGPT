using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUIController : MonoBehaviour
{
    public Image icon;
    public Text countText;
    
    public void UpdateSlot(Item item, int count)
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            countText.text = count.ToString();
            countText.enabled = count > 1;
        }
        else
        {
            icon.enabled = false;
            countText.enabled = false;
        }
    }
}