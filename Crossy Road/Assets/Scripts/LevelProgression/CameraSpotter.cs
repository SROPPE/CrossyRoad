using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CameraSpotter : MonoBehaviour
{
    
    [SerializeField] private CinemachineVirtualCamera camera;
    private PlayerMovement player;
    public event Action<Vector3> onSpotterTrigger;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onSpotterTrigger?.Invoke(player.transform.position); 
        }
    }

}
