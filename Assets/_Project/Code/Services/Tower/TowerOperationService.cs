using _Project.Code.Gameplay.Repository;
using _Project.Code.Gameplay.Tower;
using _Project.Code.Services.Wallet;
using _Project.Code.UI.View;
using UnityEngine;

namespace _Project.Code.Services.Tower
{
    public class TowerOperationService
    {
        private readonly BuildingRepository _buildingRepository;
        private readonly WalletService _walletService;
        private readonly TowerOperationView _view;

        private TowerFacade _currentTower;
        
        public TowerOperationService(
            BuildingRepository buildingRepository
            , WalletService walletService
            , TowerOperationView view)
        {
            _buildingRepository = buildingRepository;
            _walletService = walletService;
            _view = view;
            
            _view.Initialize(this);
            _view.Close();
        }

        public void Show(Building building)
        {
            _currentTower = building.GetComponent<TowerFacade>();
            _view.Show(_currentTower);
        }

        public void Upgrade()
        {
            Debug.Log("Implement Upgrade");
            _view.Close();
        }

        public void Remove()
        {
            _walletService.AddGameplayCoins(_currentTower.SellReward);
            _buildingRepository.RemoveBuilding(_currentTower.GridPosition);
            Object.Destroy(_currentTower.gameObject);

            _view.Close();
        }
    }
}