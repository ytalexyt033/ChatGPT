using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 10f;
    public float mouseSensitivity = 100f;
    public float zoomFOV = 30f;

    [Header("References")]
    public Transform playerCamera;
    public PlayerVitals vitals;

    private CharacterController controller;
    private float defaultFOV;
    private float xRotation;
    private bool isCrouching;
    private bool isZooming;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        defaultFOV = playerCamera.GetComponent<Camera>().fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleActions();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float speed = GetTargetSpeed();
        
        controller.Move(move * speed * Time.deltaTime);
    }

    private float GetTargetSpeed()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            return crouchSpeed;
        }
        
        isCrouching = false;

        if (Input.GetKey(KeyCode.LeftShift) && vitals.TryUseStamina("run"))
            return runSpeed;

        return walkSpeed;
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleActions()
    {
        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) 
            TryJump();

        // Прицеливание
        if (Input.GetMouseButtonDown(1))
            StartZoom();
        else if (Input.GetMouseButtonUp(1)) 
            StopZoom();
    }

    private void TryJump()
    {
        if (controller.isGrounded && vitals.TryUseStamina("jump"))
        {
            float jumpVelocity = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            controller.Move(Vector3.up * jumpVelocity * Time.deltaTime);
        }
    }

    private void StartZoom()
    {
        isZooming = true;
        playerCamera.GetComponent<Camera>().fieldOfView = zoomFOV;
    }

    private void StopZoom()
    {
        isZooming = false;
        playerCamera.GetComponent<Camera>().fieldOfView = defaultFOV;
    }
}