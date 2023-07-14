using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    [field: SerializeField] public ItemName Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int StackCapacity { get; private set; }
}

public enum ItemName
{
    Дерево,
    Камень
}
