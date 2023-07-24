using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowView : MonoBehaviour
{
    private TextMeshProUGUI _label;
    private TextMeshProUGUI _description;
    private Image _bigIcon;
    private Transform _itemsGridTransform;
    private ItemCellView _itemCellPrefab;
    private BuyButtonView _buyButton;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus,
        [Inject(Id = "label")] TextMeshProUGUI label,
        [Inject(Id = "description")] TextMeshProUGUI description,
        [Inject(Id = "bigIcon")] Image bigIcon,
        [Inject(Id = "itemsGridTransform")] Transform itemsGridTransform,
        ItemCellView itemCellPrefab, BuyButtonView buyButton)
    {
        _signalBus = signalBus;
        _label = label;
        _description = description;
        _bigIcon = bigIcon;
        _itemsGridTransform = itemsGridTransform;
        _itemCellPrefab = itemCellPrefab;
        _buyButton = buyButton;

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
        {
            //ItemCellView newItemCell = _itemCellFactory.Create(_itemsGridTransform);
            //newItemCell.Initialize(itemData);
            Instantiate(_itemCellPrefab, _itemsGridTransform).Initialize(itemData);
        }
    }

    private void Clear()
    {
        ItemCellView[] itemCells = _itemsGridTransform.GetComponentsInChildren<ItemCellView>();
        for (int i = 0; i < itemCells.Length; i++)
            Destroy(itemCells[i].gameObject);
        //GameObject.Destroy(itemCells[i].gameObject);
    }
}
