using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component is missing on the player!");
        }
    }

    private void Update()
    {
        // Обработка движения
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        playerMovement.Move(moveX, moveZ, isRunning);

        // Обработка прыжка
        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.Jump();
        }

        // Обработка приседания
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerMovement.Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerMovement.StandUp();
        }

        // Обработка зума камеры
        if (Input.GetMouseButtonDown(1)) // ПКМ
        {
            playerMovement.Zoom();
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerMovement.Unzoom();
        }
    }
}
