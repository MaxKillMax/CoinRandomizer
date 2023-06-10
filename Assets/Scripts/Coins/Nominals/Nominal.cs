using System;
using UnityEngine;

namespace CoinRandomizer.Coins.Nominals
{
    [Serializable]
    public struct Nominal
    {
        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;
    }
}