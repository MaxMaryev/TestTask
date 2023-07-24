using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JustMobyTestTask
{
    public class WindowInstaller : MonoInstaller
    {
        [Header("_____" + nameof(WindowModel))]
        [SerializeField] private OfferType _offerType;
        [SerializeField] private WindowDatas _windowDatas;
        [SerializeField] private ItemDatas _itemDatas;

        [Header("_____" + nameof(WindowController))]
        [SerializeField] private List<ItemsCountInput> _itemsCountInputs;
        [SerializeField] private Button _openWindowButton;
        [SerializeField] private Button _buyButton;

        [Header("_____" + nameof(WindowView))]
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _bigIcon;
        [SerializeField] private Transform _itemsGridTransform;
        [SerializeField] private ItemCellView _itemCellPrefab;
        [SerializeField] private BuyButtonView _buyButtonView;
        [SerializeField] private GameObject _windowViewGameObject;

        public override void InstallBindings()
        {
            DeclareSignals();

            BindWindowController();
            BindWindowModel();
            BindWindowView();
        }

        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ItemsCountUpdatedSignal>().RequireSubscriber();
            Container.DeclareSignal<WindowDataInitializedSignal>().RequireSubscriber();
            Container.DeclareSignal<WindowOpenedSignal>().RequireSubscriber();
            Container.DeclareSignal<BuyButtonClickedSignal>().RequireSubscriber();
        }

        private void BindWindowController()
        {
            Container.Bind<WindowController>().AsSingle().NonLazy();
            Container.Bind<List<ItemsCountInput>>().FromInstance(_itemsCountInputs);
            Container.Bind<Button>().WithId("openWindowButton").FromInstance(_openWindowButton);
            Container.Bind<Button>().WithId("buyButton").FromInstance(_buyButton);
        }

        private void BindWindowModel()
        {
            Container.Bind<WindowModel>().AsSingle().NonLazy();
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
            Container.Bind<BuyButtonView>().FromInstance(_buyButtonView);
            Container.Bind<ItemCellView>().FromInstance(_itemCellPrefab);

            Container.BindFactory<ItemCellView, WindowView.Factory>().FromComponentInNewPrefab(_itemCellPrefab);
        }

        public override void Start()
        {
            Container.Resolve<WindowController>().Initialize();
            Container.Resolve<WindowView>().Initialize();
            Container.Resolve<WindowModel>().Initialize();
        }
    }
}