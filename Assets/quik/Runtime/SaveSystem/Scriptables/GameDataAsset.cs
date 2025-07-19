using quik.Runtime.SaveSystem.Models;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "quik/Data/Game Data Asset", fileName = "GameData")]
    public class GameDataAsset : ScriptableObject
    {
        public GameData data = new();
    }
}