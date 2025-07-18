using DefaultNamespace;
using quik.Runtime.SaveSystem.Models;
using UnityEngine;

namespace quik.Runtime.SaveSystem.Interfaces
{
    public class DefaultDataService : IDefaultDataService
    {
        private readonly GameDataAsset _gameDataAsset = Resources.Load<GameDataAsset>("GameData/DefaultGameData");

        public T GetDefaultData<T>()
        {
            if (typeof(T) == typeof(GameData))
            {
                return (T)(object)GetDefaultGameData();
            }
            
            return default;
        }
        
        public GameData GetDefaultGameData()
        {
            return _gameDataAsset.data.Clone();
        }
    }
}
