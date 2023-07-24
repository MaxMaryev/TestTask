using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JustMobyTestTask
{
    [CreateAssetMenu(fileName = "ItemDatas", menuName = "SO/ItemDatas", order = 65)]
    public class ItemDatas : ScriptableObject
    {
        [SerializeField] private List<ItemData> _datas;

        public ItemData GetData(ItemName name) => _datas.FirstOrDefault(data => data.Name == name);
    }

    [Serializable]
    public class ItemData
    {
        [field: SerializeField] public ItemName Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int StackCapacity { get; private set; }
    }

    public enum ItemName
    {
        Wood,
        Stone
    }
}