using System;

namespace quik.Runtime.Localization.Interfaces
{
    /// <summary>
    /// Defines methods and properties for managing localization in an application.
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// Loads the localization data for the specified language.
        /// </summary>
        /// <param name="languageCode">The language code (e.g., "en", "fr", "de") to load.</param>
        void Load(string languageCode);
        
        /// <summary>
        /// Retrieves the localized string for the specified key.
        /// </summary>
        /// <param name="key">The key for the desired localized string.</param>
        /// <returns>The localized string associated with the given key.</returns>
        string Get(string key);
        
        /// <summary>
        /// Gets the current language code being used by the localization manager.
        /// </summary>
        string CurrentLanguage { get; }
        
        /// <summary>
        /// Occurs when the language is changed, passing the new language code.
        /// </summary>
        event Action<string> OnLanguageChanged;
    }
}