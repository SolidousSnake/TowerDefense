using UnityEngine;

namespace _Project.Code.Config
{
    [CreateAssetMenu(menuName = "_Project/Config/Sfx/Tower")]
    public class SfxConfig : ScriptableObject
    {
        [field: SerializeField] public AudioClip[] OneShotClip { get; private set; }
        [field: SerializeField] public AudioClip[] FireLoopClip { get; private set; }
        [field: SerializeField] public AudioClip[] FireTailClip { get; private set; }
    }
}