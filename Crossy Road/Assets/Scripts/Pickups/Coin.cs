using CrossyRoad.Core;
using UnityEngine;
namespace CrossyRoad.Pickups
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int coinCost = 1;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerWallet currentWallet = other.GetComponent<PlayerWallet>();
                if (currentWallet)
                {
                    currentWallet.AddCoin(coinCost);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}