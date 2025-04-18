using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public void DropItem(InventoryItem item)
    {
        if (item.worldPrefab != null)
        {
            Instantiate(item.worldPrefab, 
                      transform.position, 
                      Quaternion.identity);
        }
    }
}