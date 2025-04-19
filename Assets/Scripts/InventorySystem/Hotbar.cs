using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public static Hotbar Instance { get; private set; }
    public int CurrentSlot { get; private set; }
    public int size = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        HandleSelection();
    }

    private void HandleSelection()
    {
        for (int i = 0; i < size; i++)
            if (Input.GetKeyDown((i + 1).ToString()))
                CurrentSlot = i;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
            CurrentSlot = (CurrentSlot + (scroll > 0 ? -1 : 1) + size) % size;
    }
}