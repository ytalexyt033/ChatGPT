using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 8f;
    public float gravity = 50f;

    [Header("Camera Settings")]
    public Transform playerCamera;
    public float mouseSensitivity = 2f;
    public float maxVerticalAngle = 85f;
    public float cameraStandY = 1.6f;
    public float cameraCrouchY = 0.8f;
    public float crouchTransitionSpeed = 8f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _verticalRotation;
    private bool _isCrouching;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        if (playerCamera == null)
            playerCamera = Camera.main.transform;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (InventoryUI.Instance != null && InventoryUI.Instance.IsInventoryOpen)
        {
            _velocity.y = 0;
            return;
        }

        HandleMouseLook();
        HandleMovement();
        HandleCrouch();
        ApplyGravity();
        UpdateCameraPosition();
    }

    private void HandleMouseLook()
    {
        _verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxVerticalAngle, maxVerticalAngle);
        playerCamera.localEulerAngles = Vector3.right * _verticalRotation;

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity);
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        float speed = _isCrouching ? crouchSpeed : 
                     Input.GetKey(PlayerController.Instance.runKey) ? runSpeed : walkSpeed;

        _controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(PlayerController.Instance.crouchKey))
        {
            _isCrouching = true;
            _controller.height = crouchHeight;
            _controller.center = new Vector3(0, crouchHeight / 2, 0);
        }
        if (Input.GetKeyUp(PlayerController.Instance.crouchKey))
        {
            _isCrouching = false;
            _controller.height = standHeight;
            _controller.center = new Vector3(0, standHeight / 2, 0);
        }
    }

    public bool Jump()
    {
        if (_controller.isGrounded && !_isCrouching)
        {
            _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            return true;
        }
        return false;
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -0.1f;
        }
        else
        {
            _velocity.y -= gravity * Time.deltaTime;
        }

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void UpdateCameraPosition()
    {
        float targetY = _isCrouching ? cameraCrouchY : cameraStandY;
        Vector3 targetPos = new Vector3(transform.position.x, 
                                      transform.position.y + targetY, 
                                      transform.position.z);
        playerCamera.position = Vector3.Lerp(playerCamera.position, 
                                           targetPos, 
                                           crouchTransitionSpeed * Time.deltaTime);
    }
}