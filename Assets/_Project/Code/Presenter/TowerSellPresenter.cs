using _Project.Code.Services.Tower;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View;
using VContainer;

namespace _Project.Code.Presenter
{
    public class TowerSellPresenter
    {
        [Inject] private readonly TowerOperationView _view;
        [Inject] private readonly WalletService _walletService;
        [Inject] private readonly TowerPlacementService _placementService;
    }
}