using DG.Tweening;
using UnityEngine;

public class Log : MonoBehaviour
{
    private float movementTime;
    private Vector3 startPoint, endPoint;
    private Rigidbody rb;
    private void Awake()
    {
 
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
            Debug.Log("Exit");
            other.gameObject.transform.parent = null;
        }
    }

    private void CompleteMovement()
    {
        gameObject.SetActive(false);
    }
    public void StartMoveAction(Vector3 startPoint,Vector3 endPoint, float moveDuration)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        movementTime = moveDuration;
        transform.DOMove(endPoint, moveDuration).OnComplete(CompleteMovement);
    }
    public Vector3 GetNextGlobalPosition(Vector3 position,float moveDuretion)
    {
        float distance = Vector3.Distance(startPoint, endPoint);
        float speed = distance / movementTime;
        //Vector3 nextPosition = new Vector3(
       //     transform.DOspeed*moveDuretion)
        return Vector3.zero;
    }
}
