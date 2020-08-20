using UnityEngine;

public class DangerousObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.Dead();
        }
    }
}
