using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField] private List<Vector2> points = new List<Vector2>();
    private int _currentPointIndex = 0;

    private void Awake()
    {
        points.Clear();

        var transforms = GetComponentsInChildren<Transform>(true);
        foreach (var t in transforms)
        {
            if (t == transform) continue; // optional
            points.Add(t.position);
        }

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

    private void OnDrawGizmos()
    {
        var transforms = GetComponentsInChildren<Transform>(true);

        if (transforms.Length >= 2)
        {
            for (int i = 0, j = 1; j < transforms.Length; i++, j++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transforms[i].position, transforms[j].position);
            }

            Gizmos.DrawLine(
                transforms[transforms.Length - 1].position,
                transforms[0].position
            );
        }
    }
}