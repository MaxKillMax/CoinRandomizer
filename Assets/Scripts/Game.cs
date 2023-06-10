using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinRandomizer.Coins;
using CoinRandomizer.Coins.Nominals;
using CoinRandomizer.Droppers;
using CoinRandomizer.Times;
using DG.Tweening;
using UnityEngine;

namespace CoinRandomizer
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<Coin> _coins;
        [SerializeField] private List<Vector3> _coinsPositions;
        [SerializeField] private List<Nominal> _nominals;

        [SerializeField] private Vector3 _dropOrigin;
        [SerializeField] private Vector3 _dropDestination;
        [SerializeField] private Transform _coinBox;

        private int _order = 0;

        private readonly Tweeners.Tweener _tweener = new();
        private readonly NominalRandomizer _randomizer = new();
        private readonly CoinMixer _mixer = new();
        private Dropper _dropper;

        private void OnValidate()
        {
            while (_coinsPositions.Count < _coins.Count)
                _coinsPositions.Add(Vector3.zero);
        }

        private void Awake()
        {
            // TODO: animations and character
            //Character character = JsonConvert.DeserializeObject<Character>(_json);

            _dropper = new(_dropOrigin, _dropDestination);
        }

        private async void Start()
        {
            SetRandomNominals();
            await _dropper.DropAsync(_coins, Times.Time.COIN_DROP_TIME);
            await _tweener.ShakeAsync(_coinBox, Times.Time.COINBOX_SHAKE_TIME);

            if (this == null)
                return;

            StartCoinSelection(OnCoinMouseDowned: TrySelectCoin);
        }

        public void SetRandomNominals()
        {
            List<Nominal> nominals = _randomizer.GeUniqueRandomNominals(_nominals, _coins.Count);

            for (int i = 0; i < _coins.Count; i++)
                _coins[i].SetNominal(nominals[i]);

            _coins = _mixer.Mix(_coins);
        }

        public void StartCoinSelection(Action<Coin> OnCoinMouseDowned)
        {
            _coinBox.gameObject.SetActive(false);
            List<Coin> mixedCoins = _mixer.Mix(_coins);

            for (int i = 0; i < mixedCoins.Count; i++)
                InitializeCoin(OnCoinMouseDowned, mixedCoins[i], _coinsPositions[i]);
        }

        private void InitializeCoin(Action<Coin> OnCoinMouseDowned, Coin coin, Vector3 position)
        {
            coin.gameObject.SetActive(true);
            coin.transform.DOMove(position, Times.Time.COIN_MOVE_UP_TIME);
            coin.OnMouseDowned += () => OnCoinMouseDowned?.Invoke(coin);
        }

        private void TrySelectCoin(Coin coin)
        {
            if (_coins.IndexOf(coin) != _order)
            {
                _tweener.Jump(coin.transform, Times.Time.WRONG_COIN_SELECT_TIME);
                return;
            }

            coin.transform.position = _dropOrigin;
            coin.OnMouseDowned -= () => TrySelectCoin(coin);

            _order++;

            TryRestartAsync();
        }

        private async void TryRestartAsync()
        {
            if (_order < _coins.Count)
                return;

            _coinBox.gameObject.SetActive(true);
            _order = 0;

            await Task.Delay(Times.Time.RESTART_TIME.InMilliseconds());
            Start();
        }
    }
}