using UnityEngine;

namespace quik.Runtime.Settings.Scriptables
{
    [CreateAssetMenu(menuName = "quik/Settings/Project Settings", fileName = "NewProjectSettings")]
    public class ProjectSettingsAsset : ScriptableObject
    {
        [SerializeField] private AdSettings adSettings;
        public AdSettings AdSettings => adSettings;
        
        private const string Path = "Settings/ProjectSettings";
        
        private static ProjectSettingsAsset _instance;
        public static ProjectSettingsAsset Instance => GetInstance();

        private static ProjectSettingsAsset GetInstance()
        {
            return _instance ??= Resources.Load<ProjectSettingsAsset>(Path);
        }
    }
}
