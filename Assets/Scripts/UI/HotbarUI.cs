using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Image[] slotIcons;
    [SerializeField] private Color activeColor, inactiveColor;

    private void Update()
    {
        for (int i = 0; i < slotIcons.Length; i++)
            slotIcons[i].color = i == Hotbar.Instance.CurrentSlot ? activeColor : inactiveColor;
    }
}