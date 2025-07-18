using quik.Runtime.Localization.Interfaces;
using quik.Runtime.Services;
using TMPro;
using UnityEngine;

namespace quik.Runtime.Localization.Components
{
    public class LocalizableText : MonoBehaviour
    {
        public string key;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _text.text = ServiceLocator.Resolve<ILocalizationManager>().Get(key);
        }
    }
}