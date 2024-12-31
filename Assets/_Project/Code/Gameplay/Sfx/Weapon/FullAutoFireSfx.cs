using _Project.Code.Data.Config;
using UnityEngine;

namespace _Project.Code.Gameplay.Sfx.Weapon
{
    public class FullAutoFireSfx : WeaponSfx
    {
        private bool _isPlaying;
        
        public FullAutoFireSfx(AudioSource fireAudioSource, AudioSource tailAudioSource, SfxConfig sfxConfig) 
            : base(fireAudioSource, tailAudioSource, sfxConfig)
        {
            _isPlaying = false;
        }

        public override void PlayFire()
        {
            if (_isPlaying)
                return;

            _isPlaying = true;
            PlayFireTail();
            fireAudioSource.clip = GetRandomClip(sfxConfig.FireLoopClip);
            fireAudioSource.loop = true;
            fireAudioSource.Play();
        }

        public override void StopFire()
        {
            fireAudioSource.Stop();
            _isPlaying = false;
            fireAudioSource.loop = false;
        }
    }
}