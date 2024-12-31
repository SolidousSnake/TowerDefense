using _Project.Code.Core.SceneManagement;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using UniRx;
using UnityEngine.UI;

namespace _Project.Code.UI.UIButton
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] [Scene] private string _scene;
        [SerializeField] private Button _button;
        [Inject] private ISceneLoader _sceneLoader;

        public void Start()
        {
            _button ??= GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ => _sceneLoader.Load(_scene)).AddTo(this);
        }
    }
}