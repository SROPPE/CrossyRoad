using UnityEngine;
using DG.Tweening;
using CrossyRoad.Saving;
using CrossyRoad.Core;

namespace CrossyRoad.LevelProgression
{
    public class LevelMovement : MonoBehaviour, ISaveable
    {
        [SerializeField] private float movementSpeed;       
        [SerializeField] private float chaseTime = 0.2f;        //Time of urgent position update

        [SerializeField] private CameraSpotter cameraSpotter;   //Notifies about the need to update the position

        private bool isUrgentUpdate = false;
        private PlayerController playerController;              
        private PlayerMovement playerMovement;  

        private Vector3 startPosition;
        private void Awake()
        {
            startPosition = transform.position;
            playerController = FindObjectOfType<PlayerController>();
            playerMovement = playerController.GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            cameraSpotter.onSpotterTrigger += UrgentUpdateZPosition;
        }
        private void OnDisable()
        {
            cameraSpotter.onSpotterTrigger -= UrgentUpdateZPosition;
        }
        public void UrgentUpdateZPosition(Vector3 position)
        {
            isUrgentUpdate = true;
            transform
                .DOMoveZ(position.z, chaseTime)
                .OnComplete(() => isUrgentUpdate = false);
        }
        private void LateUpdate()
        {
            if (playerController.IsDead) return;
            if (isUrgentUpdate) return;
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            transform.position = new Vector3(playerMovement.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp
                (transform.position, transform.position + Vector3.forward, movementSpeed * Time.deltaTime);
        }

        public object CaptureState()
        {
            return new SerializableVector3(startPosition);
        }

        public void RestoreState(object state)
        {
            transform.position = ((SerializableVector3)state).ToVector();
        }
    }
}