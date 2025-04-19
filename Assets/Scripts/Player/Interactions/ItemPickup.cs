using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.instance.AddItem(item.item, item.count);
            Destroy(gameObject);
        }
    }
}