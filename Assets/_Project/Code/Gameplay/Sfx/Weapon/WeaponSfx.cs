using System.Collections.Generic;
using _Project.Code.Config;
using UnityEngine;

namespace _Project.Code.Gameplay.Sfx.Weapon
{
    public abstract class WeaponSfx
    {
        private readonly AudioSource _tailAudioSource;
        protected readonly AudioSource fireAudioSource;
        protected readonly SfxConfig sfxConfig;

        protected WeaponSfx(AudioSource fireAudioSource, AudioSource tailAudioSource, SfxConfig sfxConfig)
        {
            _tailAudioSource = tailAudioSource;
            this.fireAudioSource = fireAudioSource;
            this.sfxConfig = sfxConfig;
        }
        
        public abstract void PlayFire();
        public abstract void StopFire();

        protected void PlayFireTail() => _tailAudioSource.PlayOneShot(GetRandomClip(sfxConfig.FireTailClip));

        protected AudioClip GetRandomClip(IReadOnlyList<AudioClip> clipArray) => clipArray[Random.Range(0, clipArray.Count)];
    }
}