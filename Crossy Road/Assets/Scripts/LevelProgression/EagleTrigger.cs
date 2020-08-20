using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class EagleTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] GameObject eaglePrefab;
    [SerializeField] float eagleFlyDuration;
    [SerializeField] Transform eagleSpawnPoint;
    [SerializeField] Transform eagleEndPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            camera.Follow = player.transform;
            StartEagleEagleAction(player,player.transform.position);
        }
    }

    private void StartEagleEagleAction(PlayerController player,Vector3 position)
    {
        Vector3 spawnPoint, endPoint, playerPoint;

        SetPoints(position, out spawnPoint, out endPoint, out playerPoint);

        var eagle = Instantiate(eaglePrefab, spawnPoint, Quaternion.identity, transform);
        eagle.transform.DOMove(playerPoint, eagleFlyDuration / 2).OnComplete(()=>player.Dead());
        eagle.transform.DOMove(endPoint, eagleFlyDuration / 2);
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
