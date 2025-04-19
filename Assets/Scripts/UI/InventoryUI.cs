using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    public GameObject inventoryPanel;
    public bool IsInventoryOpen { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        IsInventoryOpen = false;
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        IsInventoryOpen = !IsInventoryOpen;
        inventoryPanel.SetActive(IsInventoryOpen);
        
        Cursor.lockState = IsInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = IsInventoryOpen;
        
        PlayerController.Instance.SetMovementLock(IsInventoryOpen);
    }
}