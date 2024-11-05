using _Project.Code.Services.TowerPlacement;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View;
using VContainer;

namespace _Project.Code.Presenter
{
    public class TowerSellPresenter
    {
        [Inject] private readonly TowerSellView _view;
        [Inject] private readonly WalletService _walletService;
        [Inject] private readonly TowerPlacementService _placementService;
    }
}