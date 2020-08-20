using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement { get; private set; }
    public bool IsDead { get; private set; } = false;
    public event Action onDeath;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void Dead()
    {
        IsDead = true;
        onDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
