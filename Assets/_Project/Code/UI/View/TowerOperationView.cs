using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using _Project.Code.Utils;

namespace _Project.Code.UI.View
{
    public class TowerOperationView : MonoBehaviour
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
            _cancelButton.OnClickAsObservable().Subscribe(_ => this.Hide()).AddTo(this);
        }

        public void Show(string name, string upgradeCost, string sellReward)
        {
            _towerNameLabel.name = name;
            _upgradeCostLabel.text = upgradeCost + _format;
            _sellRewardLabel.text = sellReward + _format;
        }

        public void Show(string name, int upgradeCost, int sellReward) =>
            Show(name, upgradeCost.ToString(), sellReward.ToString());

        private void Upgrade()
        {
            this.Hide();
        }

        private void Remove()
        {
            this.Hide();
        }
    }
}