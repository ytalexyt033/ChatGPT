using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 50f; // Мощная гравитация

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float standHeight = 2f;
    [SerializeField] private float crouchTransitionSpeed = 8f;

    [Header("Camera Settings")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxVerticalAngle = 85f;
    [SerializeField] private float cameraStandY = 1.6f;
    [SerializeField] private float cameraCrouchY = 0.8f;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _verticalRotation;
    private bool _isCrouching;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera == null)
            playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isCrouching = true;
            _controller.height = crouchHeight;
            _controller.center = new Vector3(0, crouchHeight / 2, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            _isCrouching = false;
            _controller.height = standHeight;
            _controller.center = new Vector3(0, standHeight / 2, 0);
        }

        UpdateCameraPosition();
    }

    private void HandleMouseLook()
    {
        // Вертикальный поворот камеры
        _verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxVerticalAngle, maxVerticalAngle);
        playerCamera.localEulerAngles = Vector3.right * _verticalRotation;

        // Горизонтальный поворот персонажа
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity);
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        float speed = _isCrouching ? crouchSpeed : Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        _controller.Move(move * speed * Time.deltaTime);
    }

    public void Jump() // Метод теперь публичный
    {
        if (_controller.isGrounded && !_isCrouching)
        {
            _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -0.1f; // Сбрасываем вертикальную скорость, если игрок на земле
        }
        else
        {
            _velocity.y -= gravity * Time.deltaTime; // Применяем гравитацию
        }

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void UpdateCameraPosition()
    {
        float targetY = _isCrouching ? cameraCrouchY : cameraStandY;
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + targetY, transform.position.z);
        playerCamera.position = Vector3.Lerp(playerCamera.position, targetPos, crouchTransitionSpeed * Time.deltaTime);
    }
}
