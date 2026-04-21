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
            if (t == transform) continue;
            points.Add(t.position);
        }

        if (points.Count == 0)
        {
            points.Add(Vector2.zero);
        }
    }

    public Vector2 GetNextWaypointPosition()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= points.Count)
            _currentPointIndex = 0;

        return points[_currentPointIndex];
    }

    private void OnDrawGizmos()
    {
        var transforms = GetComponentsInChildren<Transform>(true);

        if (transforms.Length < 2) return;

        Gizmos.color = Color.magenta;

        for (int i = 0; i < transforms.Length - 1; i++)
        {
            Gizmos.DrawLine(transforms[i].position, transforms[i + 1].position);
        }

        Gizmos.DrawLine(transforms[^1].position, transforms[0].position);
    }
}