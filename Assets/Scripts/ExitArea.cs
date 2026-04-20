using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArea : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private int entranceNumber = -1;

    public string GetScene()
    {
        return sceneToLoad;
    }

    public int GetSceneEntranceNumber()
    {
        return entranceNumber;
    }

    private void OnDrawGizmos()
    {
        var collider = GetComponent<PolygonCollider2D>();
        var points = collider.points;
        Vector2 center = (Vector2)transform.position + collider.offset;

        Gizmos.color = Color.yellow;

        for (int i = 0, j = 1; j < points.Length; ++i, ++j)
        {
            Gizmos.DrawLine(center + points[i], center + points[j]);
        }

        if (points.Length > 1)
        {
            Gizmos.DrawLine(center + points[1], center + points[points.Length - 1]);

        }
    }
}