using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerControls", menuName = "Input/Player Controls")]
public class InputActions : ScriptableObject
{
    // Movement
    public InputAction move { get; private set; }
    public InputAction look { get; private set; }
    public InputAction run { get; private set; }
    public InputAction crouch { get; private set; }
    public InputAction jump { get; private set; }
    
    // UI
    public InputAction inventory { get; private set; }
    
    // Camera
    public InputAction zoom { get; private set; }

    private void OnEnable()
    {
        // Movement (WASD)
        move = new InputAction("Move", InputActionType.Value);
        move.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // Camera Look (Mouse)
        look = new InputAction("Look", InputActionType.Value);
        look.AddBinding("<Mouse>/delta");

        // Run (Left Shift)
        run = new InputAction("Run", InputActionType.Button);
        run.AddBinding("<Keyboard>/leftShift");

        // Crouch (Left Ctrl)
        crouch = new InputAction("Crouch", InputActionType.Button);
        crouch.AddBinding("<Keyboard>/leftCtrl");

        // Jump (Space)
        jump = new InputAction("Jump", InputActionType.Button);
        jump.AddBinding("<Keyboard>/space");

        // Inventory (Tab)
        inventory = new InputAction("Inventory", InputActionType.Button);
        inventory.AddBinding("<Keyboard>/tab");

        // Zoom (Right Mouse Button)
        zoom = new InputAction("Zoom", InputActionType.Button);
        zoom.AddBinding("<Mouse>/rightButton");

        EnableAll();
    }

    private void OnDisable()
    {
        DisableAll();
    }

    public void EnableAll()
    {
        move?.Enable();
        look?.Enable();
        run?.Enable();
        crouch?.Enable();
        jump?.Enable();
        inventory?.Enable();
        zoom?.Enable();
    }

    public void DisableAll()
    {
        move?.Disable();
        look?.Disable();
        run?.Disable();
        crouch?.Disable();
        jump?.Disable();
        inventory?.Disable();
        zoom?.Disable();
    }
}