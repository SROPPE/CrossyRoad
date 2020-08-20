using UnityEngine;
using DG.Tweening;
public class Vehicle : MonoBehaviour,IMoveable
{
    private void CompleteMovement()
    {
        gameObject.SetActive(false);
    }

    public void StartMoveAction(Vector3 endPoint, float moveDuration)
    {
        transform.LookAt(endPoint);
        transform.DOMove(endPoint, moveDuration).OnComplete(CompleteMovement);
    }
}
