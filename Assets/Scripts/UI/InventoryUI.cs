using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    public bool IsInventoryOpen { get; private set; }
    public GameObject inventoryPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ToggleInventory()
    {
        IsInventoryOpen = !IsInventoryOpen;
        inventoryPanel.SetActive(IsInventoryOpen);
        Cursor.lockState = IsInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        PlayerController.Instance.LockMovement(IsInventoryOpen);
    }
}