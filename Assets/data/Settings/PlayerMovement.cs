using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 7f;
    public float gravity = 20f;
    public float groundCheckDistance = 0.2f;

    [Header("References")]
    public InputSettings inputSettings;
    public Transform cameraTransform;
    public LayerMask groundLayer;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _baseHeight;
    private bool _isGrounded;
    private float _rotationX;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _baseHeight = _controller.height;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        HandleCrouch();
        HandleMouseLook();
        ApplyGravity();
    }

    private void HandleGroundCheck()
    {
        Vector3 spherePos = transform.position + Vector3.down * (_controller.height / 2 - groundCheckDistance);
        _isGrounded = Physics.CheckSphere(spherePos, groundCheckDistance, groundLayer);
    }

    private void HandleMovement()
    {
        Vector2 input = new Vector2(
            Input.GetAxis(inputSettings.horizontalAxis),
            Input.GetAxis(inputSettings.verticalAxis)
        ).normalized;

        Vector3 move = transform.TransformDirection(new Vector3(input.x, 0, input.y));
        float speed = Input.GetKey(inputSettings.crouchKey) ? crouchSpeed : 
                     Input.GetKey(inputSettings.runKey) ? runSpeed : walkSpeed;

        _controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_isGrounded && Input.GetKeyDown(inputSettings.jumpKey) && !Input.GetKey(inputSettings.crouchKey))
        {
            _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
    }

    private void HandleCrouch()
    {
        bool wantCrouch = Input.GetKey(inputSettings.crouchKey);
        float targetHeight = wantCrouch ? _baseHeight * 0.5f : _baseHeight;
        _controller.height = Mathf.Lerp(_controller.height, targetHeight, 10f * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        _rotationX -= Input.GetAxis(inputSettings.mouseYAxis) * 2f;
        _rotationX = Mathf.Clamp(_rotationX, -85f, 85f);
        cameraTransform.localEulerAngles = Vector3.right * _rotationX;
        transform.Rotate(Vector3.up * Input.GetAxis(inputSettings.mouseXAxis) * 2f);
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;
        
        _velocity.y -= gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
