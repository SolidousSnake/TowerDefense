using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace _Project.Code.UI.View
{
    public class TowerSellView : BaseUI
    {
        [SerializeField] private TextMeshProUGUI _towerNameLabel;
        [SerializeField] private TextMeshProUGUI _priceLabel;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private string _format = "$";
        
        private void Awake()
        {
            _confirmButton.OnClickAsObservable().Subscribe(_ => Confirm()).AddTo(this);
            _cancelButton.OnClickAsObservable().Subscribe(_ => Hide()).AddTo(this);
        }

        public void Show(string name, string price)
        {
            _towerNameLabel.name = name;
            _priceLabel.text = price + _format;
        }

        public void Show(string name, int price) => Show(name, price.ToString());

        private void Confirm()
        {
            Hide();
        }
    }
}