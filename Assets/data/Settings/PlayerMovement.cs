using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), RequireComponent(typeof(PlayerVitals))]
public class PlayerMovement : MonoBehaviour
{
    // Serialized Fields
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

    [Header("References")]
    [SerializeField] private InventoryUI inventoryUI;
    
    // Private Variables
    private CharacterController _controller;
    private PlayerVitals _vitals;
    private InputActions _input;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private float _verticalRotation;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isCrouching;

    // Properties
    public bool IsRunning { get; private set; }
    public bool IsInventoryOpen => inventoryUI.IsOpen;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _vitals = GetComponent<PlayerVitals>();
        _input = new InputActions();

        Cursor.lockState = CursorLockMode.Locked;
        
        if (playerCamera == null)
            playerCamera = Camera.main.transform;
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    private void Update()
    {
        if (IsInventoryOpen) return;
        
        HandleMovement();
        HandleLook();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        _moveInput = _input.Player.Move.ReadValue<Vector2>();
        _isCrouching = _input.Player.Crouch.ReadValue<float>() > 0.5f;
        IsRunning = _input.Player.Run.ReadValue<float>() > 0.5f;

        Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        float speed = _isCrouching ? crouchSpeed : IsRunning ? runSpeed : walkSpeed;
        
        _controller.Move(move * (speed * Time.deltaTime));
    }

    private void HandleLook()
    {
        _lookInput = _input.Player.Look.ReadValue<Vector2>();
        
        _verticalRotation -= _lookInput.y * mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxVerticalAngle, maxVerticalAngle);
        
        playerCamera.localEulerAngles = Vector3.right * _verticalRotation;
        transform.Rotate(Vector3.up * (_lookInput.x * mouseSensitivity));
    }

    private void HandleJump()
    {
        if (_input.Player.Jump.triggered && _isGrounded && !_isCrouching)
        {
            if (_vitals.TryUseJumpStamina())
            {
                _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            }
        }
    }

    private void ApplyGravity()
    {
        _isGrounded = Physics.CheckSphere(
            transform.position + Vector3.down * (_controller.height / 2 - groundCheckRadius),
            groundCheckRadius,
            groundMask
        );

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