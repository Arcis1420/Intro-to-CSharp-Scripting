using System;
using System.Collections;
using System.Collections.Generic; //for List
using UnityEngine;

public class WaypointPath : MonoBehaviour
{

    [SerializeField] private List<Vector2> points;
    private int _currentPointIndex = 0;

    private void Awake()
    {
        var transforms = GetComponentsInChildren<Transform>(true);
        foreach (var t in transforms)
        {
            //if (t.gameObject != gameObject) //if you want to exclude the gameobject this is on
            points.Add(t.position);
        }

        //just in case there were no child objects
        //we add a single point at 0,0,0 to avoid issues
        if (points.Count <= 0)
        {
            points.Add(new Vector2(0, 0));
        }
    }

    public Vector2 GetNextWaypointPosition()
    {
        _currentPointIndex++;
        if (_currentPointIndex >= points.Count) _currentPointIndex = 0;

        return points[_currentPointIndex];
    }
}