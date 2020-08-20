using UnityEngine;
using UnityEngine.UI;

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
        playerWallet.onAddCoins += UpdateWalletShower;
    }
    private void OnDisable()
    {
        playerWallet.onAddCoins += UpdateWalletShower;
    }
    private void UpdateWalletShower()
    {
        walletShower.text = playerWallet.CoinCount.ToString();
    }
}
