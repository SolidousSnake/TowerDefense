using UnityEngine;

namespace _Project.Code.Utils
{
    public static class GameObjectExtensions
    {
        public static void Show(this Component component) => component.gameObject.SetActive(true);
        public static void Hide(this Component component) => component.gameObject.SetActive(false);
        public static void Show(this GameObject go) => go.SetActive(true);
        public static void Hide(this GameObject go) => go.SetActive(false);
    }
}