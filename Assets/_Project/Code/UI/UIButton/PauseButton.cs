using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.States;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Code.UI.UIButton
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _button;
        [Inject] private GameplayStateMachine _fsm;
        
        private void Start()
        {
            _button ??= GetComponent<UnityEngine.UI.Button>();
            _button.OnClickAsObservable().Subscribe(_ => _fsm.Enter<PauseState>()).AddTo(this);
        }
    }
}