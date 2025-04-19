using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public static Hotbar Instance { get; private set; }
    public int CurrentSlot { get; private set; }
    public int size = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseScroll();
    }

    private void HandleKeyboardInput()
    {
        for (int i = 0; i < size; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                CurrentSlot = i;
                return;
            }
        }
    }

    private void HandleMouseScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0) ChangeSlot(-1);
        else if (scroll < 0) ChangeSlot(1);
    }

    public void ChangeSlot(int direction)
    {
        CurrentSlot = (CurrentSlot + direction + size) % size;
    }
}