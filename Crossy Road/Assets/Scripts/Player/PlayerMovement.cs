using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float jumpPower = 300f;
    public UnityEvent onMoveForward;
    public event Action<Vector3> onMoveRightLeft;

    private bool isMooving = false;
    private float playerMaxPosition = 0f;
    private Rigidbody rb;
    private PlayerController playerController;

    private Vector3 OR = Vector3.zero;
    private Vector3 POS = Vector3.zero;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    public IEnumerator MoveTo(Vector3 direction, Vector3 rotation)
    {
        if (playerController.IsDead) yield break;
        if (!isMooving)
        {
            isMooving = true;
            Vector3 endPosition = Vector3.zero;
                endPosition = new Vector3
                (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z)) + direction;
            if (transform.parent != null && direction != Vector3.forward && direction != Vector3.back) 
            {

                endPosition = transform.localPosition + direction;
                DOTween.Sequence()
               .Append(transform.DOLocalJump(endPosition,jumpPower,1,moveDuration))
               .Join(transform.DOLocalRotate(rotation, moveDuration)).SetAutoKill(true);
                yield return new WaitForSeconds(moveDuration);

                isMooving = false;

                yield break;
            }

            if (CheckForObstacle(endPosition)) 
            { 
                isMooving = false;
                yield break;
                
            }

            DOTween.Sequence()
                .Append(transform.DOJump(endPosition,jumpPower,1,moveDuration))
                .Join(transform.DORotate(rotation, moveDuration)).SetAutoKill(true);
            if(direction == Vector3.right || direction == Vector3.left)
            {
                onMoveRightLeft?.Invoke(endPosition);
            }
            if (playerMaxPosition < Mathf.Round(transform.position.z) && direction == Vector3.forward)
            {                
                onMoveForward?.Invoke();
                playerMaxPosition = Mathf.Round(transform.position.z);
            }
            
            yield return new WaitForSeconds(moveDuration);

            isMooving = false;
        }
    }
    
    private bool CheckForObstacle(Vector3 position)
    {
        OR = new Vector3(position.x /2, position.y + 20, position.z/2);
        POS = new Vector3(position.x /2, position.y - 20, position.z/2);
        RaycastHit hit;
        if (Physics.Raycast(OR, POS, out hit))
        {
            
            return hit.collider.CompareTag("Obstacle");
        }
        return false;   
    }
}
