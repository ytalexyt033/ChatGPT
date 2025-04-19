using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    public List<Item> items = new List<Item>();

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

        // Инициализация предметов
        items.Add(new Item {
            id = 0,
            itemName = "Stone",
            itemType = Item.ItemType.Block
        });
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }
}