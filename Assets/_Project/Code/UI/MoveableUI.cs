using DG.Tweening;
using UnityEngine;

namespace _Project.Code.UI
{
    public abstract class MoveableUI : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private RectTransform _panel;
        [SerializeField] private Vector3 _visiblePosition;
        [SerializeField] private Vector3 _hiddenPosition;
        [SerializeField] private float _showDuration;
        [SerializeField] private float _hideDuration;
        [SerializeField] private Ease _showEase;
        [SerializeField] private Ease _hideEase;

        private void Awake() => _panel ??= GetComponent<RectTransform>();

        public virtual void Open() => 
            _panel.DOAnchorPos3D(_visiblePosition, _showDuration).SetEase(_showEase).SetLink(gameObject);

        public virtual void Close() => 
            _panel.DOAnchorPos3D(_hiddenPosition, _hideDuration).SetEase(_hideEase).SetLink(gameObject);
    }
}