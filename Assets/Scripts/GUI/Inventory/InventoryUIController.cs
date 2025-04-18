using UnityEngine;
using Player.InventorySystem;

public class InventoryUIController : MonoBehaviour
{
    [Header("References")]
    public InventorySystem inventorySystem; // Изменено с Inventory на InventorySystem
    public GameObject inventoryPanel;
    public Transform slotsParent;
    
    [Header("Settings")]
    public KeyCode toggleKey = KeyCode.Tab;
    
    private bool isOpen;

    private void Awake()
    {
        InitializeUI();
        ToggleInventory(false);
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

        for (int i = 0; i < inventorySystem.slots.Count; i++)
        {
            var slotUI = Instantiate(inventorySystem.slotUIPrefab, slotsParent);
            slotUI.Initialize(inventorySystem.slots[i]);
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}