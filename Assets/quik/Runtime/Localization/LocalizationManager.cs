using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace quik.Runtime.Localization.Interfaces
{
    public class LocalizationManager : ILocalizationManager
    {
        private Dictionary<string, string> _localizedStrings = new();
        
        public string CurrentLanguage { get; private set; }
        
        public event Action<string> OnLanguageChanged;
        
        public bool HasKey(string key)
        {
            return _localizedStrings.ContainsKey(key);
        }

        public void Load(string languageCode)
        {
            CurrentLanguage = languageCode;

            if (!TryLoadJson(languageCode, out var jsonText))
            {
                Debug.LogError($"[LocalizationManager] Localization file for '{languageCode}' not found.");
                return;
            }

            if (!TryParseJson(jsonText, out var strings))
            {
                Debug.LogError($"[LocalizationManager] Failed to parse localization file for '{languageCode}'.");
                return;
            }

            _localizedStrings = strings;
            OnLanguageChanged?.Invoke(languageCode);
        }

        public string Get(string key)
        {
            return _localizedStrings.TryGetValue(key, out var str) ? str : $"<missing:{key}>";
        }
        
        private static bool TryLoadJson(string languageCode, out string jsonText)
        {
            jsonText = null;

            TextAsset jsonFile = Resources.Load<TextAsset>($"Localization/{languageCode}");
            if (jsonFile == null)
            {
                return false;
            }

            jsonText = jsonFile.text;
            return true;
        }
        
        private static bool TryParseJson(string jsonText, out Dictionary<string, string> result)
        {
            result = null;

            try
            {
                result = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[LocalizationManager] JSON parsing error: {e.Message}");
                return false;
            }
        }
    }
}