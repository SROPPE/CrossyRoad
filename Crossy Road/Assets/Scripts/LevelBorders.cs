using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorders : MonoBehaviour
{
    [SerializeField] private Transform border;
    public static Vector3 rightBorderPosition;
    private void Awake()
    {
        rightBorderPosition = border.position;
    }
}
