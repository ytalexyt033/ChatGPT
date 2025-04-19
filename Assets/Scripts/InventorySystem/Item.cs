using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string itemName;
    public Sprite icon;
    public int maxStack = 1;
    [HideInInspector] public int currentStack = 1;

    public virtual void Use()
    {
        Debug.Log($"Used {itemName} (Stack: {currentStack})");
        currentStack--;
    }
}