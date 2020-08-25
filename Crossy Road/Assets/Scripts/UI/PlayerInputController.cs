
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrossyRoad.Core
{
    public class PlayerInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Vector2 dragOffset;
 

        private PlayerMovement playerMovement;
        private Vector3 touchPosition;

        private Dictionary<Vector2, Vector3> moveDirections = new Dictionary<Vector2, Vector3>();
        private Dictionary<Vector2, Vector3> rotateDirections = new Dictionary<Vector2, Vector3>();

        private void Awake()
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            SetRotateAndMoveDirections();
        }

        private void SetRotateAndMoveDirections()
        {
            rotateDirections.Add(Vector2.up, Vector3.zero);
            rotateDirections.Add(Vector2.left, new Vector3(0, -90, 0));
            rotateDirections.Add(Vector2.right, new Vector3(0, 90, 0));
            rotateDirections.Add(Vector2.down, new Vector3(0, 180, 0));
            moveDirections.Add(Vector2.up, Vector3.forward);
            moveDirections.Add(Vector2.down, Vector3.back);
            moveDirections.Add(Vector2.right, Vector3.right);
            moveDirections.Add(Vector2.left, Vector3.left);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Vector3 delta = Input.mousePosition - touchPosition;
            Vector2 result;

            if (Mathf.Abs(delta.x) < dragOffset.x && Mathf.Abs(delta.y) < dragOffset.y)
            {
                result = Vector2.up;
            }
            else
            {
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    result = delta.x > 0 ? Vector2.right : Vector2.left;
                }
                else
                {
                    result = delta.y > 0 ? Vector2.up : Vector2.down;
                }
            }
            StartCoroutine(playerMovement.MoveTo(moveDirections[result], rotateDirections[result]));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            touchPosition = Input.mousePosition;
        }
    }
}