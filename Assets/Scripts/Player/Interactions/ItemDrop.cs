using UnityEngine;
using Player.InventorySystem;

public class ItemDrop : MonoBehaviour
{
    public float dropForce = 5f;
    public Vector3 dropOffset = new Vector3(0, 0.5f, 0);

    public void DropItem(InventoryItem item, int amount)
    {
        if (item.worldPrefab == null) return;

        for (int i = 0; i < amount; i++)
        {
            Vector3 dropPosition = transform.position + transform.forward + dropOffset;
            GameObject droppedItem = Instantiate(item.worldPrefab, dropPosition, Quaternion.identity);
            
            if (droppedItem.TryGetComponent(out Rigidbody rb))
            {
                Vector3 forceDirection = new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    0.5f,
                    Random.Range(-0.3f, 0.3f)
                ).normalized;
                rb.AddForce(forceDirection * dropForce, ForceMode.Impulse);
            }

            var pickup = droppedItem.GetComponent<ItemPickup>();
            if (pickup == null) pickup = droppedItem.AddComponent<ItemPickup>();
            
            pickup.itemData = item;
            pickup.amount = 1;

            if (ItemEventSystem.Instance != null)
                ItemEventSystem.Instance.OnItemDropped?.Invoke(item, dropPosition);
        }
    }
}
