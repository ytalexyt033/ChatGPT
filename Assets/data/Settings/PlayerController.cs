using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Input Bindings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode inventoryKey = KeyCode.Tab;
    public KeyCode interactKey = KeyCode.E;

    private PlayerMovement _movement;
    private bool _isMovementLocked;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _movement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LockMovement(bool state) => _isMovementLocked = state;

    private void Update()
    {
        if (_isMovementLocked) return;

        if (Input.GetKeyDown(jumpKey) && _movement.Jump())
        {
            // Дополнительная логика при успешном прыжке
        }

        if (Input.GetKeyDown(inventoryKey))
            InventoryUI.Instance?.ToggleInventory();
        
        if (Input.GetKeyDown(interactKey))
            ItemInteraction.Instance?.Interact();
    }
}