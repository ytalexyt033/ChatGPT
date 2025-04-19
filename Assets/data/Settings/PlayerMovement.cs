using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 8f;
    public float gravity = 20f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float cameraCrouchOffset = 0.5f;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isCrouching;
    private Transform _cameraTransform;
    private bool _isGrounded;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (InventoryUI.Instance?.IsInventoryOpen == true) 
        {
            _velocity.y = 0;
            return;
        }

        _isGrounded = _controller.isGrounded;
        
        HandleMovement();
        ApplyGravity();
    }

    // Изменено: метод теперь public для вызова из PlayerController
    public void Jump()
    {
        if (_isGrounded && !_isCrouching)
        {
            _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
    }

    public void ToggleCrouch(bool crouch)
    {
        _isCrouching = crouch;
        _controller.height = crouch ? crouchHeight : standHeight;
        _cameraTransform.localPosition = crouch ? Vector3.down * cameraCrouchOffset : Vector3.zero;
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

    private void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -0.1f;
        }
        else
        {
            _velocity.y -= gravity * Time.deltaTime;
        }

        _controller.Move(_velocity * Time.deltaTime);
    }
}