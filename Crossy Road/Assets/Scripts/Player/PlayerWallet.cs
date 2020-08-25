using CrossyRoad.Saving;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace CrossyRoad.Core
{
    [DisallowMultipleComponent]
    public class PlayerWallet : MonoBehaviour, ISaveable
    {
        [SerializeField] private AudioSource getCoinSound;
        public event Action<int> onAddingCoins;
        public Action onAddedCoins;
        public int CoinCount { get; private set; }
        private void OnEnable()
        {
            onAddingCoins += IncreaseCoinsCount;
        }
        private void OnDisable()
        {
            onAddingCoins -= IncreaseCoinsCount;
        }
        public void AddCoin(int count)
        {
            getCoinSound.PlayOneShot(getCoinSound.clip);
            onAddingCoins?.Invoke(count);
            onAddedCoins?.Invoke();
        }
        private void IncreaseCoinsCount(int count)
        {
            CoinCount += count;
        }

        public object CaptureState()
        {
            return CoinCount;
        }

        public void RestoreState(object state)
        {
            CoinCount = (int)state;
        }
    }
}