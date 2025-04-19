using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerVitals playerVitals;

    [Header("Input Bindings")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerVitals = GetComponent<PlayerVitals>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component is missing on the player!");
        }

        if (playerVitals == null)
        {
            Debug.LogError("PlayerVitals component is missing on the player!");
        }

        // Закрепляем и скрываем курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        bool isRunning = Input.GetKey(runKey);
        bool isCrouching = Input.GetKey(crouchKey);

        // Обработка прыжка
        if (Input.GetKeyDown(jumpKey) && playerVitals.CanJump())
        {
            playerMovement.Jump(); // Теперь метод Jump доступен
            playerVitals.UseStamina(playerVitals.staminaJumpCost);
        }

        // Расход выносливости при беге
        if (isRunning)
        {
            playerVitals.UseStamina(playerVitals.staminaRunCost * Time.deltaTime);
        }
    }
}
