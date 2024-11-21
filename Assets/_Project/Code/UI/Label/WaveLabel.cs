using _Project.Code.Gameplay.Spawner;
using TMPro;
using UnityEngine;
using UniRx;

namespace _Project.Code.UI.Label
{
    public class WaveLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private string _prefix;

        private int _maxWaves;

        public void Initialize(EnemySpawner enemySpawner, int maxWaves)
        {
            enemySpawner.WaveIndex.Subscribe(SetAmount).AddTo(this);
            _maxWaves = maxWaves;
            SetAmount(0);
        }
        
        private void SetAmount(int value) => _label.text = _prefix + value + '/' + _maxWaves;
    }
}