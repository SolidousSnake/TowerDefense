using _Project.Code.Data.Config;
using UnityEngine;

namespace _Project.Code.Gameplay.Sfx.Weapon
{
    public class SemiAutoFireSfx : WeaponSfx
    {
        public SemiAutoFireSfx(AudioSource fireAudioSource, AudioSource tailAudioSource, SfxConfig sfxConfig) 
            : base(fireAudioSource, tailAudioSource, sfxConfig)
        {
        }

        public override void PlayFire()
        {
            PlayFireTail();

            fireAudioSource.clip = GetRandomClip(sfxConfig.OneShotClip);
            fireAudioSource.loop = false;
            fireAudioSource.Play();
        }

        public override void StopFire()
        {
        }
    }
}