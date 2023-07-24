using System.Collections.Generic;
using UnityEngine.UI;
using Zenject;

namespace JustMobyTestTask
{
    public class WindowController
    {
        private int _minTotalItemsCount;
        private int _maxTotalItemsCount;
        private Button _openWindowButton;
        private Button _buyButton;
        private List<ItemsCountInput> _itemsCountInputs;
        private WindowModel _windowModel;
        private Dictionary<ItemName, int> _itemCounts = new Dictionary<ItemName, int>();

        private SignalBus _signalBus;

        public WindowController(SignalBus signalBus, WindowModel windowModel,
            List<ItemsCountInput> itemsCountInputs,
            [Inject(Id = "openWindowButton")] Button openWindowButton,
            [Inject(Id = "buyButton")] Button buyButton)
        {
            _signalBus = signalBus;
            _windowModel = windowModel;
            _itemsCountInputs = itemsCountInputs;
            _openWindowButton = openWindowButton;
            _buyButton = buyButton;

            _minTotalItemsCount = 3;
            _maxTotalItemsCount = 6;

            _openWindowButton.onClick.AddListener(OnOpenButtonClick);
            _buyButton.onClick.AddListener(OnBuyButtonClick);
            _signalBus.Subscribe<BuyButtonClickedSignal>(Dispose);

            foreach (var input in _itemsCountInputs)
                input.Done += OnItemsCountInputted;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BuyButtonClickedSignal>(Dispose);
            _openWindowButton.onClick.RemoveListener(OnOpenButtonClick);

            foreach (var input in _itemsCountInputs)
                input.Done -= OnItemsCountInputted;
        }

        private void OnOpenButtonClick()
        {
            if (ValidateItemsCount() == false)
                return;

            _openWindowButton.gameObject.SetActive(false);
            _signalBus.Fire(new WindowOpenedSignal());

            foreach (var input in _itemsCountInputs)
                input.gameObject.SetActive(false);
        }

        private void OnBuyButtonClick() => _signalBus.Fire(new BuyButtonClickedSignal());

        private void OnItemsCountInputted()
        {
            _itemCounts.Clear();

            foreach (var inputField in _itemsCountInputs)
                _itemCounts.Add(inputField.ItemName, inputField.Value);

            _windowModel.UpdateItemsCount(_itemCounts);
        }

        private bool ValidateItemsCount()
        {
            int totalItemsCount = 0;

            foreach (var inputField in _itemsCountInputs)
                totalItemsCount += inputField.Value;

            return totalItemsCount >= _minTotalItemsCount && totalItemsCount <= _maxTotalItemsCount;
        }
    }
}
