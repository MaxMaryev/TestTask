using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _bigIcon;
    [SerializeField] private Transform _itemsGridTransform;
    [SerializeField] private ItemCellView _itemCellPrefab;
    [SerializeField] private BuyButtonView _buyButton;

    public void Initialize(WindowData windowData)
    {
        _label.text = windowData.Label;
        _bigIcon.sprite = windowData.BigIcon;
        _description.text = windowData.Description;
        _buyButton.DisplayPrice(windowData.Price, windowData.Discount);
    }

    public void Display(IReadOnlyList<ItemData> itemDatas)
    {
        Clear();

        foreach (var itemData in itemDatas)
            Instantiate(_itemCellPrefab, _itemsGridTransform).Initialize(itemData);
    }

    private void Clear()
    {
        ItemCellView[] itemCells = _itemsGridTransform.GetComponentsInChildren<ItemCellView>();

        for (int i = 0; i < itemCells.Length; i++)
            Destroy(itemCells[i].gameObject);
    }
}
