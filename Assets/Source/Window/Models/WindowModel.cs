using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WindowModel : MonoBehaviour
{
    private OfferType _offerType;
    private WindowDatas _windowDatas;
    private ItemDatas _itemDatas;
    private List<ItemData> _items = new List<ItemData>();
    private WindowData _windowData;
    private SignalBus _signalBus;

    [Inject]
    public void Constuct(SignalBus signalBus, OfferType offerType, WindowDatas windowDatas, ItemDatas itemDatas)
    {
        _signalBus = signalBus;
        _offerType = offerType;
        _windowDatas = windowDatas;
        _itemDatas = itemDatas;
    }

    private void OnEnable()
    {
        _windowData = _windowDatas.GetData(_offerType);
        _signalBus.Fire(new WindowDataInitializedSignal(_windowData));
    }

    public void UpdateItemsCount(IReadOnlyDictionary<ItemName, int> itemsCountInputs)
    {
        _items.Clear();

        foreach (var input in itemsCountInputs)
            InitializeItems(_itemDatas.GetData(input.Key), itemsCountInputs[input.Key]);

        _signalBus.Fire(new ItemsCountUpdatedSignal(_items));
    }

    private void InitializeItems(ItemData itemData, int count)
    {
        for (int i = 0; i < count; i++)
            _items.Add(itemData);
    }
}

