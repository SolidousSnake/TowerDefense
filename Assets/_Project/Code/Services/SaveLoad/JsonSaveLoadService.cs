using System;
using System.IO;
using _Project.Code.Data;
using _Project.Code.Utils;
using Newtonsoft.Json;
using UnityEngine.Device;


namespace _Project.Code.Services.SaveLoad
{
    public class JsonSaveLoadService : ISaveLoadService
    {
        private readonly string _filePath = Application.persistentDataPath + Constants.PlayerProgress;
        
        public void Save(WalletData progress)
        {
            string json = JsonConvert.SerializeObject(progress);
            File.WriteAllText(_filePath, json);
        }
        
        public WalletData Load()
        {
            if (!File.Exists(_filePath))
                return new WalletData();
        
            string json = File.ReadAllText(_filePath);
            WalletData progress = JsonConvert.DeserializeObject<WalletData>(json);
            return progress;
        }

        public void Reset() => Save(new WalletData());
    }
}