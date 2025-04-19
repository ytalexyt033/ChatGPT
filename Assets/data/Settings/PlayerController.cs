using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode inventoryKey = KeyCode.Tab;

    private PlayerMovement _movement;

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

        _movement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (InventoryUI.Instance?.IsInventoryOpen == true) return;

        if (Input.GetKeyDown(jumpKey))
            _movement.Jump();

        if (Input.GetKeyDown(inventoryKey))
            InventoryUI.Instance?.ToggleInventory();
    }
}