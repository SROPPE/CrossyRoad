using Cinemachine;
using CrossyRoad.Core;
using System;
using UnityEngine;
namespace CrossyRoad.LevelProgression
{
    public class CameraSpotter : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camera;
        public event Action<Vector3> onSpotterTrigger;

        private PlayerMovement player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerMovement>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onSpotterTrigger?.Invoke(player.transform.position);
            }
        }

    }
}