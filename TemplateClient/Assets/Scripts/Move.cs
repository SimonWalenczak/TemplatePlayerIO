using System.Collections;
using System.Collections.Generic;
//using Pathfinding;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] float _cellSize;
    [SerializeField] Transform _textParent;

    public int NBPath = 1000;
    int nb_calculate = 0;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }
    }

    public static Vector3 GetMouseWorldPos()
    {
        Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }
    public static Vector3 GetMouseWorldPosWithZ(Vector3 screenPos, Camera worldCam)
    {
        Vector3 worldPos = worldCam.ScreenToWorldPoint(screenPos);
        return worldPos;
    }
}
