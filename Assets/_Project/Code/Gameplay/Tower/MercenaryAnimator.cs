using _Project.Code.Utils;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _Project.Code.Gameplay.Tower
{
    [RequireComponent(typeof(TowerFacade))]
    public class MercenaryAnimator : MonoBehaviour
    {
        [SerializeField] private TowerFacade _towerFacade;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rig _aimLayer;
        [SerializeField] private float _rigSelectionDuration;
        
        private void Start()
        {
            _towerFacade ??= GetComponent<TowerFacade>();

            _towerFacade.EnemySighted += OnEnemySighted;
            _towerFacade.EnemyLost += OnEnemyLost;
        }

        private void OnEnemySighted()
        {
            _animator.SetBool(AnimationParameters.Mercenary.EnemySighted, true);
            _aimLayer.weight += Time.deltaTime / _rigSelectionDuration;
        }

        private void OnEnemyLost()
        {
            _animator.SetBool(AnimationParameters.Mercenary.EnemySighted, false);
            _aimLayer.weight -= Time.deltaTime / _rigSelectionDuration;
        }
        
        private void OnDestroy()
        {
            _towerFacade.EnemySighted -= OnEnemySighted;
            _towerFacade.EnemyLost -= OnEnemyLost;
        }
    }
}