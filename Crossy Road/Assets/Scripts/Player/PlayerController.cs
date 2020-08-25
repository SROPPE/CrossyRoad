using CrossyRoad.Saving;
using System;
using UnityEngine;

namespace CrossyRoad.Core
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsDead { get; private set; } = true;
        public event Action onDeath;
        private Vector3 startPosition;
        private void Awake()
        {
            startPosition = transform.position;
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


    }
}