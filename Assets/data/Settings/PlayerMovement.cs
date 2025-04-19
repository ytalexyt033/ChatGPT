using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 8f;
    public float gravity = 20f;
    public float mouseSensitivity = 2f;
    public float maxVerticalAngle = 85f;

    [Header("Crouch")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float cameraStandY = 1.6f;
    public float cameraCrouchY = 0.8f;

    private CharacterController _controller;
    private Transform _camera;
    private Vector3 _velocity;
    private float _verticalRotation;
    private bool _isCrouching;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (InventoryUI.Instance?.IsInventoryOpen == true) return;

        HandleMouseLook();
        HandleMovement();
        HandleCrouch();
        ApplyGravity();
        UpdateCameraHeight();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation - mouseY, -maxVerticalAngle, maxVerticalAngle);
        _camera.localEulerAngles = Vector3.right * _verticalRotation;
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
        }
        else if (Input.GetKeyUp(PlayerController.Instance.crouchKey))
        {
            _isCrouching = false;
            _controller.height = standHeight;
        }
    }

    private void UpdateCameraHeight()
    {
        float targetY = _isCrouching ? cameraCrouchY : cameraStandY;
        Vector3 camPos = _camera.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, targetY, Time.deltaTime * 10f);
        _camera.localPosition = camPos;
    }

    public void Jump()
    {
        if (_controller.isGrounded && !_isCrouching)
        {
            _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y = -0.1f;
        else
            _velocity.y -= gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}