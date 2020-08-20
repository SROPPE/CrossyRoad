using UnityEngine;
using DG.Tweening;
public class Vehicle : MonoBehaviour
{
    private void CompleteMovement()
    {
        gameObject.SetActive(false);
    }
    public void StartMoveAction(Vector3 endPoint, float currentGivenSpeed)
    {
      
        transform.DOMove(endPoint, currentGivenSpeed).OnComplete(CompleteMovement);
    }
}
