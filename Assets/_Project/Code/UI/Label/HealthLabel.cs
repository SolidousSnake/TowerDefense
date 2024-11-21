using TMPro;
using UnityEngine;

namespace _Project.Code.UI.Label
{
    public class HealthLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private string _prefix;

        public void SetAmount(float value) => _label.text = _prefix + value;
    }
}