using _Project.Code.Core.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtainCanvasGroup;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _percentLabel;

        [Inject] private ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            Hide();
            SetProgress(0);

            _sceneLoader.OnLoadingStarted += Show;
            _sceneLoader.OnLoadingProgress += SetProgress;
            _sceneLoader.OnLoadingCompleted += Hide;
        }

        private void Show()
        {
            _curtainCanvasGroup.alpha = 1;
            _curtainCanvasGroup.blocksRaycasts = true;
        }

        private void Hide()
        {
            _curtainCanvasGroup.alpha = 0;
            _curtainCanvasGroup.blocksRaycasts = false;
        }

        private void SetProgress(float progress)
        {
            _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, progress, Time.deltaTime * 5f);
            _percentLabel.text = Mathf.RoundToInt(progress * 100) + "%";
        }

        private void OnDestroy()
        {
            _sceneLoader.OnLoadingStarted -= Show;
            _sceneLoader.OnLoadingProgress -= SetProgress;
            _sceneLoader.OnLoadingCompleted -= Hide;
        }
    }
}