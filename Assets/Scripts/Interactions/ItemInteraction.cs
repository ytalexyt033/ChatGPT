using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public static ItemInteraction Instance { get; private set; }

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactableLayers;

    private Camera _camera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start() => _camera = Camera.main;

    public void Interact()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayers))
            Debug.Log($"Interacted with {hit.collider.name}");
    }
}