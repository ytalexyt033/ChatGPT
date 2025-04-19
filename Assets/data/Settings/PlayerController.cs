using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Input Bindings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode inventoryKey = KeyCode.Tab;

    private PlayerMovement movement;
    private bool isMovementLocked;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        movement = GetComponent<PlayerMovement>();
    }

    public void SetMovementLock(bool state)
    {
        isMovementLocked = state;
    }

    private void Update()
    {
        if (isMovementLocked) return;

        if (Input.GetKeyDown(jumpKey))
        {
            movement.Jump();
        }
    }
}