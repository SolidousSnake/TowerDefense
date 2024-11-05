using UnityEngine;

namespace _Project.Code.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}