using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float crouchHeight = 1f; // Высота при приседании
    public float zoomFOV = 40f; // Поле зрения при зуме

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Camera playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning;
    private bool isCrouching;
    private bool isZoomed;
    private float originalHeight;
    private float originalFOV;

    private PlayerVitals playerVitals;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerVitals = GetComponent<PlayerVitals>();

        if (controller == null)
        {
            Debug.LogError("CharacterController component is missing on the player!");
        }

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck object is not assigned in the inspector!");
        }

        originalHeight = controller.height;
        originalFOV = playerCamera.fieldOfView;
    }

    private void Update()
    {
        // Проверка, находится ли игрок на земле
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Гравитация
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Move(float moveX, float moveZ, bool isRunning)
    {
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float speed = isRunning && playerVitals.CanRun() ? runSpeed : walkSpeed;

        if (isRunning)
        {
            playerVitals.UseStamina(playerVitals.staminaRunCost * Time.deltaTime);
        }

        controller.Move(move * speed * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded && playerVitals.CanJump())
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            playerVitals.UseStamina(playerVitals.staminaJumpCost);
        }
    }

    public void Crouch()
    {
        isCrouching = true;
        controller.height = crouchHeight;
    }

    public void StandUp()
    {
        isCrouching = false;
        controller.height = originalHeight;
    }

    public void Zoom()
    {
        isZoomed = true;
        playerCamera.fieldOfView = zoomFOV;
    }

    public void Unzoom()
    {
        isZoomed = false;
        playerCamera.fieldOfView = originalFOV;
    }
}
