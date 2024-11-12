using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace _Project.Code.UI.View
{
    public class TowerOperationView : BaseUI
    {
        [SerializeField] private TextMeshProUGUI _towerNameLabel;
        [SerializeField] private TextMeshProUGUI _upgradeCostLabel;
        [SerializeField] private TextMeshProUGUI _sellRewardLabel;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private string _format = "$";

        private void Awake()
        {
            _upgradeButton.OnClickAsObservable().Subscribe(_ => Upgrade()).AddTo(this);
            _removeButton.OnClickAsObservable().Subscribe(_ => Remove()).AddTo(this);
            _cancelButton.OnClickAsObservable().Subscribe(_ => Hide()).AddTo(this);
        }

        public void Show(string name, string upgradeCost, string sellReward)
        {
            base.Show();
            _towerNameLabel.name = name;
            _upgradeCostLabel.text = upgradeCost + _format;
            _sellRewardLabel.text = sellReward + _format;
        }

        public void Show(string name, int upgradeCost, int sellReward) =>
            Show(name, upgradeCost.ToString(), sellReward.ToString());

        private void Upgrade()
        {
            Hide();
        }

        private void Remove()
        {
            Hide();
        }
    }
}