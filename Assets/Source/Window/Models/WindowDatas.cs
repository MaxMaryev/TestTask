using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WindowDatas", menuName = "SO/WindowDatas", order = 66)]
public class WindowDatas : ScriptableObject
{
    [SerializeField] private List<WindowData> _datas;

    public WindowData GetData(OfferType offerType) => _datas.FirstOrDefault(data => data.OfferType == offerType);
}

[Serializable]
public class WindowData
{
    [field: SerializeField] public OfferType OfferType { get; private set; }
    [field: SerializeField] public string Label { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
    [field: SerializeField] public float Discount { get; private set; }
    [field: SerializeField] public Sprite BigIcon { get; private set; }
}

public enum OfferType
{
    ResourcesForBuilding,
    BeginnerBuilderKit
}