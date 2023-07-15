using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    [SerializeField] private List<ItemsCountInput> _itemsCountInputs;
    [SerializeField] private int _minTotalItemsCount;
    [SerializeField] private int _maxTotalItemsCount;
    [Space(20)]
    [SerializeField] private Button _openWindowButton;
    [SerializeField] private WindowModel _windowModel;
    [SerializeField] private WindowView _windowView;

    private Dictionary<ItemName, int> _itemCounts = new Dictionary<ItemName, int>();

    private void OnEnable()
    {
        _openWindowButton.onClick.AddListener(OnOpenButtonClick);

        foreach (var input in _itemsCountInputs)
            input.Done += OnItemsCountInputted;
    }

    private void OnDisable()
    {
        _openWindowButton.onClick.RemoveListener(OnOpenButtonClick);

        foreach (var input in _itemsCountInputs)
            input.Done -= OnItemsCountInputted;
    }

    private void OnOpenButtonClick()
    {
        if (ValidateItemsCount() == false)
            return;

        _openWindowButton.gameObject.SetActive(false);
        _windowView.gameObject.SetActive(true);

        foreach (var input in _itemsCountInputs)
            input.gameObject.SetActive(false);
    }

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
