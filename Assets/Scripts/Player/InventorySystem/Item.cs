using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;
    public int maxStack = 1;
    public bool isDefaultItem = false;
}