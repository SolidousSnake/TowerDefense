using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.View.State.Gameplay
{
    public class VictoryStateView : MonoBehaviour
    {
        [SerializeField] private Button _loadMenuButton;
        [SerializeField] private Star[] _stars;

        [SerializeField] private string _bonusRewardPrefix;
        [SerializeField] private TextMeshProUGUI _bonusRewardLabel;

        [SerializeField] private string _totalRewardPrefix;
        [SerializeField] private TextMeshProUGUI _totalRewardLabel;
        
        private UniTaskCompletionSource<TargetStates> _result;

        public UniTask<TargetStates> Open()
        {
            gameObject.SetActive(true);
            _result = new UniTaskCompletionSource<TargetStates>();
            return _result.Task;
        }

        public void SetBonusReward(int value) => _bonusRewardLabel.text = _bonusRewardPrefix + $"{value}";
        public void SetTotalReward(int value) => _totalRewardLabel.text = _totalRewardPrefix + $"{value}";

        public void ToggleStars(int value)
        {
            value = Mathf.Clamp(value, 0, _stars.Length);
            
            for (int i = 0; i < value; i++) 
                _stars[i].Toggle(i < value);
        }

        private void OnEnable() => 
            _loadMenuButton.OnClickAsObservable().Subscribe(_ => _result.TrySetResult(TargetStates.LoadMenu)).AddTo(this);
    }
}