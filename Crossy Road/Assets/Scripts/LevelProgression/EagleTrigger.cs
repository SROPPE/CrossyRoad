using Cinemachine;
using CrossyRoad.Core;
using DG.Tweening;
using UnityEngine;
namespace CrossyRoad.LevelProgression
{
    public class EagleTrigger : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camera;

        [SerializeField] GameObject eaglePrefab;
        [SerializeField] Transform eagleSpawnPoint;
        [SerializeField] Transform eagleEndPoint;
        [SerializeField] float eagleFlyDuration;

        private Transform defaultFollowObject;
        private void Awake()
        {
            defaultFollowObject = camera.Follow;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();
                camera.Follow = player.transform;

                StartEagleAction(player, player.transform.position);
            }
        }

        private void StartEagleAction(PlayerController player, Vector3 position)
        {
            player.Dead();

            Vector3 spawnPoint, endPoint, playerPoint;

            SetPoints(position, out spawnPoint, out endPoint, out playerPoint);

            var eagle = Instantiate(eaglePrefab, spawnPoint, Quaternion.identity, transform);

            eagle.transform
                .DOMove(playerPoint, eagleFlyDuration / 2);
            eagle.transform
                .DOMove(endPoint, eagleFlyDuration / 2)
                .OnComplete(() => { camera.Follow = defaultFollowObject; Destroy(eagle); });
        }

        private void SetPoints(Vector3 position, out Vector3 spawnPoint, out Vector3 endPoint, out Vector3 playerPoint)
        {
            spawnPoint = eagleSpawnPoint.position;
            endPoint = eagleEndPoint.position;

            playerPoint = new Vector3(position.x, spawnPoint.y, position.z);
            spawnPoint = new Vector3(position.x, spawnPoint.y, spawnPoint.z);
            endPoint = new Vector3(position.x, endPoint.y, endPoint.z);
        }
    }
}