using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowInstaller : MonoInstaller
{
    [Header("_____" + nameof(WindowModel))]
    [SerializeField] private OfferType _offerType;
    [SerializeField] private WindowDatas _windowDatas;
    [SerializeField] private ItemDatas _itemDatas;

    [Header("_____" + nameof(WindowController))]
    [SerializeField] private List<ItemsCountInput> _itemsCountInputs;
    [SerializeField] private Button _openWindowButton;

    [Header("_____" + nameof(WindowView))]
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _bigIcon;
    [SerializeField] private Transform _itemsGridTransform;
    [SerializeField] private ItemCellView _itemCellPrefab;
    [SerializeField] private BuyButtonView _buyButton;
    [SerializeField] private GameObject _windowViewGameObject;


    public override void InstallBindings()
    {
        BindWindowController();
        BindWindowModel();
        BindWindowView();

        DeclareSignals();
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ItemsCountUpdatedSignal>().RequireSubscriber();
        Container.DeclareSignal<WindowDataInitializedSignal>().RequireSubscriber();
        Container.DeclareSignal<WindowOpenedSignal>().RequireSubscriber();
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
        Container.Bind<WindowView>().AsSingle().NonLazy();
        Container.Bind<TextMeshProUGUI>().WithId("label").FromInstance(_label);
        Container.Bind<TextMeshProUGUI>().WithId("description").FromInstance(_description);
        Container.Bind<Image>().WithId("bigIcon").FromInstance(_bigIcon);
        Container.Bind<Transform>().WithId("itemsGridTransform").FromInstance(_itemsGridTransform);
        Container.Bind<GameObject>().WithId("windowViewGameObject").FromInstance(_windowViewGameObject);
        Container.Bind<BuyButtonView>().FromInstance(_buyButton);
        Container.Bind<ItemCellView>().FromInstance(_itemCellPrefab);

        Container.BindFactory<ItemCellView, WindowView.Factory>().FromComponentInNewPrefab(_itemCellPrefab);
    }

    private void BindWindowController()
    {

        Container.Bind<List<ItemsCountInput>>().FromInstance(_itemsCountInputs);
        Container.Bind<Button>().FromInstance(_openWindowButton);
    }
}