using UnityEngine;

[CreateAssetMenu(fileName = "InputSettings", menuName = "Inventory/Input Settings")]
public class InputSettings : ScriptableObject
{
    [Header("Movement")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Camera")]
    public string mouseXAxis = "Mouse X";
    public string mouseYAxis = "Mouse Y";
    public KeyCode zoomKey = KeyCode.Mouse1;

    [Header("Inventory")]
    public KeyCode toggleInventory = KeyCode.Tab;
    public KeyCode[] hotbarKeys = new KeyCode[6]
    {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6
    };

    [Header("Interaction")]
    public KeyCode useItemKey = KeyCode.Mouse0;
    public KeyCode quickMoveKey = KeyCode.Mouse1;
    public KeyCode splitStackKey = KeyCode.LeftControl;
}
