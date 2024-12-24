using _Project.Code.Gameplay.Tower;
using _Project.Code.Services.Tower;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using _Project.Code.Utils;
using Alchemy.Inspector;

namespace _Project.Code.UI.View
{
    public class TowerOperationView : MoveableUI
    {
        [Title("Common")]
        [SerializeField] private TextMeshProUGUI _towerNameLabel;
        [SerializeField] private TextMeshProUGUI _upgradeCostLabel;
        [SerializeField] private TextMeshProUGUI _sellRewardLabel;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private string _format = "$";

       private TowerOperationService _service;

        public void Initialize(TowerOperationService service)
        {
            _service = service;
            _upgradeButton.OnClickAsObservable().Subscribe(_ => Upgrade()).AddTo(this);
            _removeButton.OnClickAsObservable().Subscribe(_ => Remove()).AddTo(this);
            _cancelButton.OnClickAsObservable().Subscribe(_ => Close()).AddTo(this);
        }

        public void Show(TowerFacade tower)
        {
            Open();
            _towerNameLabel.name = tower.Name;
            _upgradeCostLabel.text = tower.UpgradeCost + _format;
            _sellRewardLabel.text = tower.SellReward + _format;
        }

        private void Upgrade()
        {
            Close();
            _service.Upgrade();
        }

        private void Remove()
        {
            Close();
            _service.Remove();
        }
    }
}