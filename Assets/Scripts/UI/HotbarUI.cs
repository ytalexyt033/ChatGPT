using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Image[] _slots;
    [SerializeField] private Color _activeColor = Color.yellow;
    [SerializeField] private Color _normalColor = Color.gray;

    private void Update()
    {
        if (Hotbar.Instance == null || _slots == null || _slots.Length == 0)
            return;

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].color = i == Hotbar.Instance.CurrentSlot ? _activeColor : _normalColor;
        }
    }
}