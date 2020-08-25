using UnityEngine;

public class RaycastPositionChecker : MonoBehaviour
{
    public static bool Check(Vector3 position, string compareTag)
    {
        Vector3 upPosition = new Vector3(position.x, position.y + 20, position.z);
        RaycastHit hit;
        if (Physics.Raycast(upPosition, Vector3.down, out hit))
        {
            return hit.collider.CompareTag(compareTag);
        }
        return false;
    }
}
