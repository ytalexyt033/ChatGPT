using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public Item GetItemByID(string itemID)
    {
        return items.Find(item => item.itemID == itemID);
    }
}