using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowController : MonoBehaviour
{
    [SerializeField] private WindowView _windowView;

    private int _minTotalItemsCount = 3;
    private int _maxTotalItemsCount = 6;
    private Button _openWindowButton;
    private List<ItemsCountInput> _itemsCountInputs;
    private WindowModel _windowModel;

    private Dictionary<ItemName, int> _itemCounts = new Dictionary<ItemName, int>();

    [Inject]
    public void Construct(WindowModel windowModel, List<ItemsCountInput> itemsCountInputs,
        Button openWindowButton)
    {
        _windowModel = windowModel;
        _itemsCountInputs = itemsCountInputs;
        _openWindowButton = openWindowButton;
    }

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

    //public void Initialize()
    //{
    //    _openWindowButton.onClick.AddListener(OnOpenButtonClick);

    //    foreach (var input in _itemsCountInputs)
    //        input.Done += OnItemsCountInputted;
    //}

    //public void Dispose()
    //{
    //    _openWindowButton.onClick.RemoveListener(OnOpenButtonClick);

    //    foreach (var input in _itemsCountInputs)
    //        input.Done -= OnItemsCountInputted;
    //}
}
