using UnityEngine;
using Player.InventorySystem;

public class ItemEventSystem : MonoBehaviour
{
    public static ItemEventSystem Instance { get; private set; }

    public ItemPickupEvent OnItemPickedUp = new ItemPickupEvent();
    public ItemDropEvent OnItemDropped = new ItemDropEvent();
    public ItemUseEvent OnItemUsed = new ItemUseEvent();

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
        }
    }
}
