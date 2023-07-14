using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Transform _itemsGridTransform;
    [SerializeField] private ItemCellView _itemCellPrefab;
    [SerializeField] private BuyButtonView _buyButton;
    [SerializeField] private ItemsSO _itemsSO;

    public void Initialize(string label, string description, float price, float discount)
    {
        _label.text = label;
        _description.text = description;
        _buyButton.DisplayPrice(price, discount);
    }

    public void Display(int stoneCount, int woodCount)
    {
        Clear();
        int totalItemsCount = stoneCount + woodCount;

        if (ValidateItemsCount(totalItemsCount) == false)
            return;

        for (int i = 0; i < stoneCount; i++)
        {
            Instantiate(_itemCellPrefab, _itemsGridTransform)
                .Initialize(_itemsSO.GetDataByName(ItemName.Камень));
        }

        for (int i = 0; i < woodCount; i++)
        {
            Instantiate(_itemCellPrefab, _itemsGridTransform)
                .Initialize(_itemsSO.GetDataByName(ItemName.Дерево));
        }
    }

    private void Clear()
    {
        List<ItemCellView> itemCells = _itemsGridTransform.GetComponentsInChildren<ItemCellView>().ToList();

        for (int i = 0; i < itemCells.Count; i++)
        {
            Destroy(itemCells[i].gameObject);
        }
    }

    private bool ValidateItemsCount(int itemsCount) => itemsCount >= 3 && itemsCount <= 6;
}
