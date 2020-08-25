using CrossyRoad.Saving;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace CrossyRoad.Core
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerMovement : MonoBehaviour, ISaveable
    {
        [SerializeField] private float moveDuration = 1f;
        [SerializeField] private float jumpPower = 300f;
        [SerializeField] private float dwellingTime = 0.01f;
        [SerializeField] private AudioSource jumpSound;
        public UnityEvent onGetNewZPosition;
        public event Action<Vector3> onMoveRightLeft;

        private bool isMooving = false;
        private float playerMaxPosition = 0f;               //The maximum Z to which the player got in the current race
        private PlayerController playerController;
        private Vector3 startPosition;

        private void Awake()
        {
            startPosition = transform.position;
            playerController = GetComponent<PlayerController>();
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

                if (RaycastPositionChecker.Check(endPosition, "Obstacle"))
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

                    yield return new WaitForSeconds(moveDuration + dwellingTime);
                    Debug.Log(dwellingTime);
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
                onGetNewZPosition?.Invoke();
                playerMaxPosition = Mathf.Round(transform.position.z);
            }
        }
        private void StopMove()
        {
            isMooving = false;
        }

        public object CaptureState()
        {
            return new SerializableVector3(startPosition);
        }

        public void RestoreState(object state)
        {
            playerMaxPosition = startPosition.z;
        }
    }
}