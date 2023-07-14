using System.Collections.Generic;
using UnityEngine;

public class WindowModel : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private string _description;
    [SerializeField] private float _price;
    [SerializeField] private float _discount;
    [SerializeField] private WindowView _windowView;

    private void OnEnable()
    {
        _windowView.Initialize(_label, _description, _price, _discount);
    }

    public void UpdateCellsCount(int stoneCount, int woodCount)
    {
        _windowView.Display(stoneCount, woodCount);
    }
}

