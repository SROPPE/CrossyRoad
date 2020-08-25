using UnityEngine;

namespace CrossyRoad.Obstacles
{
    public class LevelBorders : MonoBehaviour
    {
        [SerializeField] private Transform rightBorder;     
        public static Vector3 rightBorderPosition;
        private void Awake()
        {
            rightBorderPosition = rightBorder.position;
        }
    }
}
