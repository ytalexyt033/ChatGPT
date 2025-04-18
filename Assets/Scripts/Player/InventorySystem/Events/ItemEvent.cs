using UnityEngine;
using UnityEngine.Events;

namespace Player.InventorySystem
{
    [System.Serializable]
    public class ItemEvent : UnityEvent<InventoryItem> {}

    [System.Serializable]
    public class ItemPickupEvent : UnityEvent<InventoryItem> {}

    [System.Serializable]
    public class ItemDropEvent : UnityEvent<InventoryItem, Vector3> {}

    [System.Serializable]
    public class ItemUseEvent : UnityEvent<InventoryItem> {}
}
