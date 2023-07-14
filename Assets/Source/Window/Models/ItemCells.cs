using UnityEngine;

public class ItemCells
{ 
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public ItemData ItemData { get; private set; }

    public ItemCells(ItemData itemData, int count)
    {
        ItemData = itemData;
        Count = count;
    }
}
