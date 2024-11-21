using _Project.Code.Config;
using UnityEngine;

namespace _Project.Code.Gameplay.Sfx
{
    public class TowerSfx
    {
        private readonly AudioSource _fireAudioSource;
        private readonly AudioSource _tailAudioSource;
        private readonly SfxConfig _sfxConfig;
        private bool _isPlaying;

        public TowerSfx(AudioSource fireAudioSource, AudioSource tailAudioSource, SfxConfig sfxConfig)
        {
            _fireAudioSource = fireAudioSource;
            _sfxConfig = sfxConfig;
            _tailAudioSource = tailAudioSource;
            _isPlaying = false;
        }
        
        public void PlayFire()
        {
            if (_isPlaying)
                return;

            _isPlaying = true;
            _fireAudioSource.PlayOneShot(GetRandomFireClip(_sfxConfig.OneShotClip));
            _fireAudioSource.clip = GetRandomFireClip(_sfxConfig.FireClip);
            
            _fireAudioSource.loop = true;
            _fireAudioSource.Play();
        }

        public void StopFire()
        {
            _fireAudioSource.Stop();
            _fireAudioSource.loop = false;
            _isPlaying = false;
        }
        
        private AudioClip GetRandomFireClip(AudioClip[] clipArray) => clipArray[Random.Range(0, clipArray.Length)];
    }
}