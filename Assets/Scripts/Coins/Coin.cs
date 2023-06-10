using System;
using CoinRandomizer.Coins.Nominals;
using UnityEngine;

namespace CoinRandomizer.Coins
{
    [RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Collider2D))]
    public class Coin : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public Nominal Nominal { get; private set; }

        public event Action OnMouseDowned;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetNominal(Nominal nominal)
        {
            Nominal = nominal;
            _spriteRenderer.sprite = Nominal.Sprite;
        }

        private void OnMouseDown() => OnMouseDowned?.Invoke();
    }
}