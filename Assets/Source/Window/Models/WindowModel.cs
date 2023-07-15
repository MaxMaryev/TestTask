using System.Collections.Generic;
using UnityEngine;

public class WindowModel : MonoBehaviour
{
    [SerializeField] private OfferType _offerType;
    [Space(20)]
    [SerializeField] private WindowDatas _windowDatas;
    [SerializeField] private ItemDatas _itemDatas;
    [SerializeField] private WindowView _windowView;

    private List<ItemData> _items = new List<ItemData>();

    private WindowData _windowData;

    private void OnEnable()
    {
        _windowData = _windowDatas.GetData(_offerType);
        _windowView.Initialize(_windowData);
    }

    public void UpdateItemsCount(IReadOnlyDictionary<ItemName, int> itemsCountInputs)
    {
        _items.Clear();

        foreach (var input in itemsCountInputs)
            InitializeItems(_itemDatas.GetData(input.Key), itemsCountInputs[input.Key]);

        _windowView.Display(_items);
    }

    private void InitializeItems(ItemData itemData, int count)
    {
        for (int i = 0; i < count; i++)
            _items.Add(itemData);
    }
}

