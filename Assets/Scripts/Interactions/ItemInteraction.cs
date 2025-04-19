using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask interactableLayers;

    private Camera _camera;

    private void Start() => _camera = Camera.main;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) UseItem();
        if (Input.GetMouseButtonDown(1)) Interact();
    }

    private void UseItem()
    {
        var item = InventorySystem.Instance?.GetHotbarItem(Hotbar.Instance.CurrentSlot);
        item?.Use();
    }

    private void Interact()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, interactDistance, interactableLayers))
            Debug.Log($"Interacted with {hit.collider.name}");
    }
}