using System.IO;
using Newtonsoft.Json;
using quik.Runtime.SaveSystem.Converters;
using quik.Runtime.SaveSystem.Interfaces;
using quik.Runtime.SaveSystem.Models;
using quik.Runtime.Services;
using UnityEngine;

namespace quik.Runtime.SaveSystem.Services
{
    public class JsonSaveService : ISaveService
    {
        private const int CurrentVersion = 1;
        
        private readonly string _savePath = Application.persistentDataPath;
        
        private readonly IDefaultDataService _defaultDataService = ServiceLocator.Resolve<IDefaultDataService>();
        
        private readonly JsonSerializerSettings _settings = new()
        {
            Formatting = Formatting.Indented,
            Converters =
            {
                new Vector2Converter(),
                new Vector3Converter(),
                new QuaternionConverter(),
                new TransformConverter()
            }
        };
        
        public void Save<T>(string key, T data)
        {
            var path = Path.Combine(_savePath, key + ".json");
            var backupPath = Path.Combine(_savePath, key + ".json.bak");

            // Backup existing save
            if (File.Exists(path))
            {
                File.Copy(path, backupPath, overwrite: true);
            }

            var json = JsonConvert.SerializeObject(data, _settings);
            File.WriteAllText(path, json);
        }

        public T Load<T>(string key)
        {
            var path = Path.Combine(_savePath, key + ".json");

            if (!File.Exists(path))
            {
                var defaultData = _defaultDataService.GetDefaultData<T>();
                Save(key, defaultData);
                return defaultData;
            }
            
            var json = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<T>(json, _settings);

            // Check if data supports versioning and needs migration
            if (data is not GameData saveData || typeof(T) != typeof(GameData))
            {
                return data;
            }

            if (saveData.meta.saveVersion >= CurrentVersion) return data;
            saveData = MigrateSaveData(saveData);
            return (T)(object)saveData;
        }

        public bool HasKey(string key)
        {
            return File.Exists(Path.Combine(_savePath, key + ".json"));
        }

        public void Delete(string key)
        {
            var path = Path.Combine(_savePath, key + ".json");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void DeleteAll()
        {
            var dir = new DirectoryInfo(_savePath);
            foreach (var file in dir.GetFiles("*.json"))
            {
                file.Delete();
            }
        }

        private GameData MigrateSaveData(GameData oldData)
        {
            Debug.Log($"[SaveSystem] Migrating from version {oldData.meta.saveVersion} to {CurrentVersion}");

            if (oldData.meta.saveVersion == 1)
            {
                // Example migration logic
                oldData.meta.saveVersion = 2;
                // Modify or initialize new fields as needed
            }

            return oldData;
        }
    }
}