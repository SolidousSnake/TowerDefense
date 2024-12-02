using System.IO;
using _Project.Code.Data.PersistentProgress;
using _Project.Code.Utils;
using Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.Device.Application;


namespace _Project.Code.Services.SaveLoad
{
    public class JsonSaveLoadService : ISaveLoadService
    {
        private readonly string _filePath = Application.persistentDataPath + Constants.PlayerProgress;

        public void Save(PlayerProgress progress)
        {
            string json = JsonConvert.SerializeObject(progress);
            File.WriteAllText(_filePath, json);
        }
        
        public PlayerProgress Load()
        {
            if (!File.Exists(_filePath))
                return new PlayerProgress();
        
            string json = File.ReadAllText(_filePath);
            PlayerProgress progress = JsonConvert.DeserializeObject<PlayerProgress>(json);
            return progress;
        }

        public void Reset() => Save(new PlayerProgress());
    }
}