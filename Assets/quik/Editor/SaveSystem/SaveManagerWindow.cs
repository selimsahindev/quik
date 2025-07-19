using System.IO;
using UnityEditor;
using UnityEngine;

namespace quik.Editor.SaveSystem
{
    public class SaveManagerWindow : EditorWindow
    {
        private Vector2 _fileListScroll;
        private Vector2 _contentScroll;
        private string[] _saveFiles;
        private string _selectedFile;
        private string _fileContent;
        
        [MenuItem("quik./Save Manager")]
        public static void Open()
        {
            GetWindow<SaveManagerWindow>("Save Manager");
        }
        
        private void OnEnable()
        {
            RefreshSaveFiles();
        }

        private void RefreshSaveFiles()
        {
            var dir = Application.persistentDataPath;
            _saveFiles = Directory.GetFiles(dir, "*.json");
        }
        
        private void OnGUI()
        {
            GUILayout.Label("Save Files", EditorStyles.boldLabel);

            if (GUILayout.Button("Refresh", GUILayout.Width(310)))
            {
                RefreshSaveFiles();
            }

            if (GUILayout.Button("Delete All Saves", GUILayout.Width(310)))
            {
                OnDeleteAllSavesButtonClicked();
            }

            GUILayout.Space(10);
            GUILayout.Label("Files", EditorStyles.boldLabel);

            DrawSaveFilesScrollView();

            if (!string.IsNullOrEmpty(_selectedFile))
            {
                GUILayout.Space(10);
                GUILayout.Label("File Content", EditorStyles.boldLabel);
                _contentScroll = EditorGUILayout.BeginScrollView(_contentScroll, GUILayout.Height(720));
                _fileContent = EditorGUILayout.TextArea(_fileContent, GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();
            }
        }

        private void DrawSaveFilesScrollView()
        {
            _fileListScroll = EditorGUILayout.BeginScrollView(_fileListScroll, GUILayout.Height(200));

            for (int i = 0; i < _saveFiles.Length; i++)
            {
                string file = _saveFiles[i];
                string fileName = Path.GetFileName(file);

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(fileName, GUILayout.Width(250)))
                {
                    _selectedFile = file;
                    _fileContent = File.ReadAllText(file);
                }

                bool deleted = false;

                if (GUILayout.Button("Delete", GUILayout.Width(60)))
                {
                    if (EditorUtility.DisplayDialog("Are you sure?", $"Delete {fileName}?", "Delete", "Cancel"))
                    {
                        File.Delete(file);
                        deleted = true;
                        if (_selectedFile == file)
                        {
                            _selectedFile = null;
                            _fileContent = string.Empty;
                        }
                    }
                }

                EditorGUILayout.EndHorizontal();

                if (deleted)
                {
                    RefreshSaveFiles();
                    break; // now safe after EndHorizontal
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void OnDeleteAllSavesButtonClicked()
        {
            if (EditorUtility.DisplayDialog("Delete All?", "Are you sure you want to delete all save files?", "Delete", "Cancel"))
            {
                foreach (var file in _saveFiles)
                {
                    File.Delete(file);
                }
                RefreshSaveFiles();
                _fileContent = string.Empty;
                _selectedFile = null;
            }
        }
    }
}
