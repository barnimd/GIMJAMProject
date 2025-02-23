using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // Referensi ke data item

    public ItemData GetItemData()
    {
        return itemData;
    }
}
