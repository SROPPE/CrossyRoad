using UnityEngine;
using DG.Tweening;
namespace CrossyRoad.Obstacles
{
    public class Vehicle : MonoBehaviour, IMoveableObstacle
    {
        public void StartMoveAction(Vector3 endPoint, float moveDuration)
        {
            transform.LookAt(endPoint);
            transform.DOMove(endPoint, moveDuration).OnComplete(CompleteMovement);
        }
        private void CompleteMovement()
        {
            gameObject.SetActive(false);
        }
    }
}