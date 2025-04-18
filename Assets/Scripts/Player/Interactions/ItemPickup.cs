using UnityEngine;
using Player.InventorySystem;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    public InventoryItem itemData;
    public int amount = 1;
    public float pickupRadius = 1.5f;
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<Inventory>();
        if (inventory == null) return;

        if (inventory.AddItem(new InventoryItem(itemData), amount))
        {
            if (ItemEventSystem.Instance != null)
                ItemEventSystem.Instance.OnItemPickedUp?.Invoke(itemData);
            
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
