using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowInstaller : MonoInstaller
{
    [SerializeField] private OfferType _offerType;
    [SerializeField] private WindowDatas _windowDatas;
    [SerializeField] private ItemDatas _itemDatas;
    [Space(20)]
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _bigIcon;
    [SerializeField] private Transform _itemsGridTransform;
    [SerializeField] private ItemCellView _itemCellPrefab;
    [SerializeField] private BuyButtonView _buyButton;
    [Space(20)]
    [SerializeField] private List<ItemsCountInput> _itemsCountInputs;
    [SerializeField] private int _minTotalItemsCount;
    [SerializeField] private int _maxTotalItemsCount;
    [SerializeField] private Button _openWindowButton;
    [SerializeField] private WindowModel _windowModel;
    [SerializeField] private WindowView _windowView;
    public override void InstallBindings()
    {
        Container.Bind<WindowView>().AsSingle();
        BindWindowModel();
        DeclareSignals();
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ItemsCountUpdatedSignal>().RequireSubscriber();
        Container.DeclareSignal<WindowDataInitializedSignal>().RequireSubscriber();
    }

    private void BindWindowModel()
    {
        Container.Bind<WindowModel>().AsSingle();
        Container.Bind<OfferType>().FromInstance(_offerType).AsSingle();
        Container.Bind<WindowDatas>().FromInstance(_windowDatas).AsSingle();
        Container.Bind<ItemDatas>().FromInstance(_itemDatas).AsSingle();
    }

    private void BindWindowView()
    {
      
    }

    private void BindWindowController()
    {

    }
}