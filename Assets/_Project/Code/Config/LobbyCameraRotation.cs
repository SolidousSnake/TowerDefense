using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Config
{
    [CreateAssetMenu(menuName = "_Project/UI/CameraRotation")]
    public class LobbyCameraRotation : ScriptableObject
    {
        [field: SerializeField] public Vector3 Rotation { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}