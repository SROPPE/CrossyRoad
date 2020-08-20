using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float jumpPower = 300f;
    public UnityEvent onMoveForward;
    public event Action<Vector3> onMoveRightLeft;

    private bool isMooving = false;
    public float playerMaxPosition { get; set; } = 0f;
    private Rigidbody rb;
    private PlayerController playerController;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        playerController.onDeath += StopMove;
    }
    private void OnDisable()
    {
        playerController.onDeath -= StopMove;
    }
    public IEnumerator MoveTo(Vector3 direction, Vector3 rotation)
    {
        if (playerController.IsDead) yield break;
        if (!isMooving)
        {
            isMooving = true;
            Vector3 endPosition = new Vector3
                (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z)) + direction;

            if (CheckOfPosition(endPosition, "Obstacle"))
            {
                isMooving = false;
                yield break;

            }

            jumpSound.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            jumpSound.Play();

            if (transform.parent != null && direction != Vector3.forward && direction != Vector3.back)
            {
                
                endPosition = transform.localPosition + direction;

                DOTween.Sequence()
               .Append(transform.DOLocalJump(endPosition, jumpPower, 1, moveDuration))
               .Join(transform.DOLocalRotate(rotation, moveDuration)).SetAutoKill(true);
                yield return new WaitForSeconds(moveDuration);
               
                isMooving = false;

                yield break;
            }

            DOTween.Sequence()
                .Append(transform.DOJump(endPosition, jumpPower, 1, moveDuration))
                .Join(transform.DORotate(rotation, moveDuration)).SetAutoKill(true);

            CallMoveDirectionEvents(direction, endPosition);

            yield return new WaitForSeconds(moveDuration);

            isMooving = false;
        }
    }

    private void CallMoveDirectionEvents(Vector3 direction, Vector3 endPosition)
    {
        if (direction == Vector3.right || direction == Vector3.left)
        {
            onMoveRightLeft?.Invoke(endPosition);
        }
        if (playerMaxPosition < Mathf.Round(transform.position.z) && direction == Vector3.forward)
        {
            onMoveForward?.Invoke();
            playerMaxPosition = Mathf.Round(transform.position.z);
        }
    }

    private bool CheckOfPosition(Vector3 position, string compareTag)
    {
        Vector3 upPosition = new Vector3(position.x /2, position.y + 20, position.z/2);
        Vector3 downPosition = new Vector3(position.x /2, position.y - 20, position.z/2);
        RaycastHit hit;
        if (Physics.Raycast(upPosition,downPosition, out hit))
        {     
            return hit.collider.CompareTag(compareTag);
        }
        return false;   
    }
    private void StopMove()
    {
        isMooving = false;
    }
}
