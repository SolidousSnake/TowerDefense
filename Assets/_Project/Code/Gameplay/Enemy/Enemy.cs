using _Project.Code.Config;
using _Project.Code.Gameplay.Unit;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace _Project.Code.Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject _originalPrefab;
        [SerializeField] private GameObject _destroyedPrefab;
        [SerializeField] private int _deathTime;

        private Health _health;

        public void Initialize(EnemyConfig config)
        {
            _originalPrefab.SetActive(true);
            _destroyedPrefab.SetActive(false);

            _health = new Health(config.MaxHealth);

            _health.Points.Where(hp => hp <= 0).Subscribe(_ => ApplyDeath().Forget()).AddTo(this);
        }

        private async UniTaskVoid ApplyDeath()
        {
            _originalPrefab.SetActive(false);
            _destroyedPrefab.SetActive(true);
            await UniTask.Delay(_deathTime);
            Destroy(gameObject);
        }
    }
}