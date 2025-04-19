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
    public KeyCode hotbarNextKey = KeyCode.Q;
    public KeyCode hotbarPrevKey = KeyCode.Z;

    [Header("References")]
    [SerializeField] private PlayerMovement _movement;
    private bool _isMovementLocked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (_movement == null)
            _movement = GetComponent<PlayerMovement>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LockMovement(bool state) => _isMovementLocked = state;

    private void Update()
    {
        if (_isMovementLocked) return;

        // Движение и прыжки
        if (Input.GetKeyDown(jumpKey))
            _movement.Jump();

        // Инвентарь
        if (Input.GetKeyDown(inventoryKey))
            ToggleInventory();

        // Горячие клавиши хотбара
        if (Input.GetKeyDown(hotbarNextKey))
            Hotbar.Instance?.SelectNextSlot();
        else if (Input.GetKeyDown(hotbarPrevKey))
            Hotbar.Instance?.SelectPrevSlot();

        // Взаимодействие
        if (Input.GetKeyDown(interactKey))
            ItemInteraction.Instance?.Interact();
    }

    private void ToggleInventory()
    {
        if (InventoryUI.Instance != null)
        {
            bool newState = !InventoryUI.Instance.IsInventoryOpen;
            InventoryUI.Instance.ToggleInventory();
            
            // Дополнительные действия при открытии/закрытии
            if (newState)
            {
                // Логика при открытии инвентаря
            }
            else
            {
                // Логика при закрытии инвентаря
            }
        }
        else
        {
            Debug.LogWarning("InventoryUI instance is missing!");
        }
    }
}