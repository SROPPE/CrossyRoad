using DG.Tweening;
using UnityEngine;

namespace CrossyRoad.Obstacles
{
    public class Log : MonoBehaviour, IMoveableObstacle
    {
        private void CompleteMovement()
        {
            gameObject.SetActive(false);
        }
        public void StartMoveAction(Vector3 endPoint, float moveDuration)
        {
            transform.DOMove(endPoint, moveDuration).OnComplete(CompleteMovement);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.parent = transform;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.parent = null;
            }
        }
    }
}