using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Image[] slots;
    [SerializeField] private Color activeColor, inactiveColor;

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].color = i == Hotbar.Instance.CurrentSlot ? activeColor : inactiveColor;
    }
}