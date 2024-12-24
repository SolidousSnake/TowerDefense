using _Project.Code.Gameplay.Unit;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Bar
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _insideScale;
        [SerializeField] private Color _lowHealthColor;
        [SerializeField] private Color _fullHealthColor;

        private Health _health;

        public void Initialize(Health health)
        {
            _health = health;
            _health.Points.Subscribe(SetHealthAmount).AddTo(this);
        }

        private void SetHealthAmount(float amount)
        {
            _insideScale.fillAmount = amount / _health.MaxHealth;
            _insideScale.color = Color.Lerp(_lowHealthColor, _fullHealthColor, _insideScale.fillAmount);
        }
    }
}