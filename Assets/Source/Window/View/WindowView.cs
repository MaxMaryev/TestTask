using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JustMobyTestTask
{
    public class WindowView
    {
        private GameObject _windowViewGameObject;
        private TextMeshProUGUI _label;
        private TextMeshProUGUI _description;
        private Image _bigIcon;
        private Transform _itemsGridTransform;
        private BuyButtonView _buyButton;

        private SignalBus _signalBus;
        private Factory _itemCellFactory;

        public WindowView(SignalBus signalBus, Factory itemCellFactory,
            [Inject(Id = "label")] TextMeshProUGUI label,
            [Inject(Id = "description")] TextMeshProUGUI description,
            [Inject(Id = "bigIcon")] Image bigIcon,
            [Inject(Id = "itemsGridTransform")] Transform itemsGridTransform,
            [Inject(Id = "windowViewGameObject")] GameObject windowViewGameObject,
            BuyButtonView buyButton)
        {
            _signalBus = signalBus;
            _itemCellFactory = itemCellFactory;
            _label = label;
            _description = description;
            _bigIcon = bigIcon;
            _itemsGridTransform = itemsGridTransform;
            _buyButton = buyButton;
            _windowViewGameObject = windowViewGameObject;

            _signalBus.Subscribe<ItemsCountUpdatedSignal>(OnItemsCountUpdated);
            _signalBus.Subscribe<WindowDataInitializedSignal>(OnWindowDataInitialized);
            _signalBus.Subscribe<WindowOpenedSignal>(OnWindowOpened);
            _signalBus.Subscribe<BuyButtonClickedSignal>(OnWindowClosed);
        }

        public void OnWindowDataInitialized(WindowDataInitializedSignal signal)
        {
            _label.text = signal.WindowData.Label;
            _bigIcon.sprite = signal.WindowData.BigIcon;
            _description.text = signal.WindowData.Description;
            _buyButton.DisplayPrice(signal.WindowData.Price, signal.WindowData.Discount);
        }

        public void OnItemsCountUpdated(ItemsCountUpdatedSignal signal)
        {
            Clear();

            foreach (var itemData in signal.ItemDatas)
            {
                ItemCellView newItemCell = _itemCellFactory.Create();
                newItemCell.transform.SetParent(_itemsGridTransform, worldPositionStays: false);
                newItemCell.Initialize(itemData);
            }
        }

        private void Clear()
        {
            ItemCellView[] itemCells = _itemsGridTransform.GetComponentsInChildren<ItemCellView>();
            for (int i = 0; i < itemCells.Length; i++)
                GameObject.Destroy(itemCells[i].gameObject);
        }

        private void OnWindowOpened() => _windowViewGameObject.SetActive(true);

        private void OnWindowClosed()
        {
            _signalBus.Unsubscribe<ItemsCountUpdatedSignal>(OnItemsCountUpdated);
            _signalBus.Unsubscribe<WindowDataInitializedSignal>(OnWindowDataInitialized);
            _signalBus.Unsubscribe<WindowOpenedSignal>(OnWindowOpened);
            _signalBus.Unsubscribe<BuyButtonClickedSignal>(OnWindowClosed);
        }

        public class Factory : PlaceholderFactory<ItemCellView>
        { }
    }
}
