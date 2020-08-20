using DG.Tweening;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour,ISaveable
{
    public PlayerMovement playerMovement { get; private set; }
    public bool IsDead { get; private set; } = true;
    public event Action onDeath;
    private Vector3 startPosition;
    private void Awake()
    {
        startPosition = transform.position;
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void CanPlay()
    {
        IsDead = false;
        transform.position = startPosition;
        gameObject.SetActive(true);
    }
    public void Dead()
    {
        IsDead = true;
        onDeath?.Invoke();
        transform.parent = null;
        gameObject.SetActive(false);
    }

    public object CaptureState()
    {
        return new SerializableVector3(startPosition);
    }

    public void RestoreState(object state)
    {
        playerMovement.playerMaxPosition = startPosition.z;
    }
   
}
