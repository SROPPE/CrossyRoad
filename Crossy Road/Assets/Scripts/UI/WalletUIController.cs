using CrossyRoad.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CrossyRoad.UI
{
    public class WalletUIController : MonoBehaviour
    {
        [SerializeField] private Text walletShower;

        private PlayerWallet playerWallet;

        private void Awake()
        {
            playerWallet = FindObjectOfType<PlayerWallet>();
        }
        private void Start()
        {
            walletShower.text = playerWallet.CoinCount.ToString();
        }
        private void OnEnable()
        {
            playerWallet.onAddedCoins += UpdateWalletShower;
        }
        private void OnDisable()
        {
            playerWallet.onAddedCoins += UpdateWalletShower;
        }
        private void UpdateWalletShower()
        {
            walletShower.text = playerWallet.CoinCount.ToString();
        }
    }
}