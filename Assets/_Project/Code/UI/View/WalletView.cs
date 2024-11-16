using _Project.Code.Services.Wallet;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Code.UI.View
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private string _format = "$";
        
        [Inject] private WalletService _service;

        private void Start() => 
            _service.GameplayCoins.Subscribe(coins => _label.text = coins + _format).AddTo(this);
    }
}
