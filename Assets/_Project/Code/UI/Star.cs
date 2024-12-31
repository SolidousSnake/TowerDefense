using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _filledSprite;
        [SerializeField] private Sprite _emptySprite;
        
        public void Toggle(bool filled) => _image.sprite = filled ? _filledSprite : _emptySprite;
    }
}