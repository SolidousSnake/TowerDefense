using _Project.Code.Services.Wallet;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Code.UI.Label
{
    public class MenuCoinsLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private string _suffix = "$";
        
        [Inject]
        private void Construct(WalletService walletService) => walletService.MenuCoins.Subscribe(SetAmount).AddTo(this);
        
        private void SetAmount(int value) => _label.text = value + _suffix;
    }
}