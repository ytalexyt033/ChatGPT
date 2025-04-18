using UnityEngine;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(PlayerVitals))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 30f;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundMask;

    [Header("Camera Settings")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxVerticalAngle = 85f;
    [SerializeField] private float cameraStandHeight = 1.6f;
    [SerializeField] private float cameraCrouchHeight = 0.8f;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomFOV = 30f;
    [SerializeField] private float zoomSpeed = 10f;

    [Header("Input")]
    [SerializeField] private InputActions inputActions;

    private CharacterController _controller;
    private PlayerVitals _vitals;
    private Camera _mainCamera;
    private float _defaultFOV;
    private float _verticalRotation;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isCrouching;
    private bool _isZooming;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _vitals = GetComponent<PlayerVitals>();
        _mainCamera = Camera.main;
        _defaultFOV = _mainCamera.fieldOfView;

        if (playerCamera == null)
            playerCamera = _mainCamera.transform;

        inputActions.EnableAll();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        inputActions.DisableAll();
    }

    private void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleLook();
        HandleJump();
        HandleCrouch();
        HandleZoom();
        HandleInventoryToggle();
        ApplyGravity();
    }

    private void HandleGroundCheck()
    {
        _isGrounded = Physics.CheckSphere(
            transform.position + Vector3.down * (_controller.height / 2 - groundCheckRadius),
            groundCheckRadius,
            groundMask
        );
    }

    private void HandleMovement()
    {
        Vector2 moveInput = inputActions.move.ReadValue<Vector2>();
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;

        float currentSpeed = _isCrouching ? crouchSpeed : 
                           inputActions.run.IsPressed() ? runSpeed : walkSpeed;

        _controller.Move(moveDirection * (currentSpeed * Time.deltaTime));
    }

    private void HandleLook()
    {
        Vector2 lookInput = inputActions.look.ReadValue<Vector2>() * mouseSensitivity;

        _verticalRotation -= lookInput.y;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        playerCamera.localEulerAngles = Vector3.right * _verticalRotation;
        transform.Rotate(Vector3.up * lookInput.x);
    }

    private void HandleJump()
    {
        if (inputActions.jump.triggered && _isGrounded && !_isCrouching)
        {
            if (_vitals.TryUseJumpStamina())
            {
                _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            }
        }
    }

    private void HandleCrouch()
    {
        _isCrouching = inputActions.crouch.IsPressed();
        float targetHeight = _isCrouching ? cameraCrouchHeight : cameraStandHeight;
        playerCamera.localPosition = Vector3.up * Mathf.Lerp(
            playerCamera.localPosition.y, 
            targetHeight, 
            Time.deltaTime * 10f
        );
    }

    private void HandleZoom()
    {
        _isZooming = inputActions.zoom.IsPressed();
        float targetFOV = _isZooming ? zoomFOV : _defaultFOV;
        _mainCamera.fieldOfView = Mathf.Lerp(
            _mainCamera.fieldOfView, 
            targetFOV, 
            Time.deltaTime * zoomSpeed
        );
    }

    private void HandleInventoryToggle()
    {
        if (inputActions.inventory.triggered)
        {
            bool shouldLockCursor = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = shouldLockCursor ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        else
        {
            _velocity.y -= gravity * Time.deltaTime;
        }
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position + Vector3.down * (_controller.height / 2 - groundCheckRadius),
            groundCheckRadius
        );
    }
}