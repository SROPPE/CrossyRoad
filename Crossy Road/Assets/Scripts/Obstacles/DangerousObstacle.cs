using CrossyRoad.Core;
using UnityEngine;
namespace CrossyRoad.Obstacles
{
    public class DangerousObstacle : MonoBehaviour
    {
        [SerializeField] private AudioSource deathSound;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();
                if (!player.IsDead)
                {
                    deathSound.Play();
                    player.Dead();
                }
            }
        }
    }
}