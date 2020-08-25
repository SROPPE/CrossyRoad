using UnityEngine;

namespace CrossyRoad.Obstacles
{
    public interface IMoveableObstacle
    {
        void StartMoveAction(Vector3 endPoint, float moveDuration);

    }
}