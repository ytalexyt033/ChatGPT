using UnityEngine;
using Player.InventorySystem;

namespace Player.InventorySystem
{
    public class HotbarSystem : MonoBehaviour
    {
        [SerializeField] private int _size = 5;
        private InventoryItem[] _items;
        private int _selectedIndex;

        public int SelectedIndex => _selectedIndex;

        private void Awake()
        {
            _items = new InventoryItem[_size];
        }

        public InventoryItem GetSlot(int index)
        {
            return _items[index];
        }

        public void SelectSlot(int index)
        {
            _selectedIndex = Mathf.Clamp(index, 0, _size - 1);
        }

        public bool AddItem(InventoryItem item, int count)
        {
            // Логика добавления предмета
            return true;
        }
    }
}
