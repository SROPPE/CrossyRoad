using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinCost = 1;
    private PlayerWallet currentWallet;
    private void Awake()
    {
        currentWallet = FindObjectOfType<PlayerWallet>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentWallet.AddCoin(coinCost);
            gameObject.SetActive(false);
        }
    }
}
