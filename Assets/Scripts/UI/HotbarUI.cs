using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.gray;

    private void Update()
    {
        if (Hotbar.Instance == null || slotImages == null) return;

        for (int i = 0; i < slotImages.Length; i++)
        {
            bool isSelected = i == Hotbar.Instance.CurrentSlot;
            slotImages[i].color = isSelected ? selectedColor : normalColor;
            
            // Обновление иконок предметов
            var item = InventorySystem.Instance?.GetHotbarItem(i);
            if (item != null)
            {
                slotImages[i].sprite = item.icon;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].enabled = false;
            }
        }
    }
}