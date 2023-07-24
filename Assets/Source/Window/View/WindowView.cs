using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _bigIcon;
    [SerializeField] private Transform _itemsGridTransform;
    [SerializeField] private ItemCellView _itemCellPrefab;
    [SerializeField] private BuyButtonView _buyButton;

    private SignalBus _signalBus;

    [Inject]
    public void Constuct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<ItemsCountUpdatedSignal>(Display);
        _signalBus.Subscribe<WindowDataInitializedSignal>(Initialize);
    }

    public void Initialize(WindowDataInitializedSignal signal)
    {
        _label.text = signal.WindowData.Label;
        _bigIcon.sprite = signal.WindowData.BigIcon;
        _description.text = signal.WindowData.Description;
        _buyButton.DisplayPrice(signal.WindowData.Price, signal.WindowData.Discount);
    }

    public void Display(ItemsCountUpdatedSignal signal)
    {
        Clear();

        foreach (var itemData in signal.ItemDatas)
            Instantiate(_itemCellPrefab, _itemsGridTransform).Initialize(itemData);
    }

    private void Clear()
    {
        ItemCellView[] itemCells = _itemsGridTransform.GetComponentsInChildren<ItemCellView>();
        for (int i = 0; i < itemCells.Length; i++)
            Destroy(itemCells[i].gameObject);
    }
}
