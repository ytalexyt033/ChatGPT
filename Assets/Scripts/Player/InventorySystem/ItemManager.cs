using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemDatabase database;
    
    public void AddItemToInventory(string itemID)
    {
        Item item = database.GetItemByID(itemID);
        if (item != null)
        {
            Inventory.instance.AddItem(item);
        }
    }
}