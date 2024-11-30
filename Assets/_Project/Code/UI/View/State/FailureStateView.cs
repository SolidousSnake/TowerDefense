using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.View.State
{
    public class FailureStateView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _loadMenuButton;
        
        private UniTaskCompletionSource<TargetStates> _result;

        public UniTask<TargetStates> Open()
        {
            gameObject.SetActive(true);
            _result = new UniTaskCompletionSource<TargetStates>();
            return _result.Task; 
        }

        private void OnEnable()
        {
            _restartButton.OnClickAsObservable().Subscribe(_ => _result.TrySetResult(TargetStates.Restart)).AddTo(this);
            _loadMenuButton.OnClickAsObservable().Subscribe(_ => _result.TrySetResult(TargetStates.LoadMenu)).AddTo(this);
        }
    }
}