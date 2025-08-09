using quik.Runtime.UI.PanelSystem;
using quik.Runtime.UI.PanelSystem.Enums;
using UnityEngine;

namespace quik.Runtime.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PanelManager panelManager;
        
        public void OpenMainMenu()
        {
            panelManager.OpenPanel(PanelKey.MainMenu.GetKey());
        }

        public void OpenSettings()
        {
            panelManager.OpenPanel(PanelKey.Settings.GetKey());
        }

        public void CloseSettings()
        {
            panelManager.ClosePanel(PanelKey.Settings.GetKey());
        }

        public void BackToPreviousPanel()
        {
            panelManager.BackToPreviousPanel();
        }
    }
}