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
        [SerializeField] private TextMeshProUGUI _rewardLabel;
        [SerializeField] private string _prefix;
        
        private UniTaskCompletionSource<TargetStates> _result;

        public UniTask<TargetStates> Open()
        {
            gameObject.SetActive(true);
            _result = new UniTaskCompletionSource<TargetStates>();
            return _result.Task;
        }

        public void SetReward(int value) => _rewardLabel.text = _prefix + $"{value}";

        private void OnEnable()
        {
            _loadMenuButton.OnClickAsObservable().Subscribe(_ => _result.TrySetResult(TargetStates.LoadMenu)).AddTo(this);
        }
    }
}