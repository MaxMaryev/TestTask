using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemUI", menuName = "ItemUI", order = 65)]
public class ItemsSO : ScriptableObject
{
    [SerializeField] private List<ItemData> _datas;

    public ItemData GetDataByName(ItemName name)
    {
        return _datas.FirstOrDefault(data => data.Name == name);
    }
}