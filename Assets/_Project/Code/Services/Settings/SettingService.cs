using _Project.Code.Services.SaveLoad;
using UniRx;
using UnityEngine;

namespace _Project.Code.Services.Settings
{
    public class SettingService
    {
        private const int DefaultFrameRate = 30;

        private readonly ISaveLoadService _saveLoadService;
        
        public ReactiveProperty<int> TargetFrameRate { get; private set; }

        public SettingService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            TargetFrameRate = new ReactiveProperty<int>(saveLoadService.Load().SettingData.FrameRate);
            ApplyTargetFrameRate(DefaultFrameRate);

            TargetFrameRate.Subscribe(ApplyTargetFrameRate);
        }

        private void ApplyTargetFrameRate(int frameRate)
        {
            Application.targetFrameRate = frameRate;
            
            var data = _saveLoadService.Load();
            data.SettingData.FrameRate = frameRate;
            _saveLoadService.Save(data);
        }
    }
}