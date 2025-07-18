using System;
using quik.Runtime.Core.Interfaces;

namespace quik.Runtime.SaveSystem.Models
{
    [Serializable]
    public class CurrencyData : ICloneable<CurrencyData>
    {
        public int coins;
        public int gems;
        
        public CurrencyData Clone()
        {
            return new CurrencyData
            {
                coins = coins,
                gems = gems
            };
        }
    }
}