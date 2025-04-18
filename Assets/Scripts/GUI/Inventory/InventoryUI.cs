using UnityEngine;
using UnityEngine.UI;
using Player.InventorySystem;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    public GameObject inventoryPanel;
    public Transform slotsParent;
    public InventorySlotUI slotPrefab;
    
    [Header("Settings")]
    public KeyCode toggleKey = KeyCode.Tab;
    
    private Inventory inventory;
    private bool isOpen;
    
    public bool IsOpen => isOpen;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        InitializeUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    private void InitializeUI()
    {
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < inventory.capacity; i++)
        {
            Instantiate(slotPrefab, slotsParent).Initialize(inventory.slots[i]);
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}