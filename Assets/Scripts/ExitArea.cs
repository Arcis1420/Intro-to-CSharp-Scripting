using UnityEngine;

public class ExitArea : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private int entranceNumber = -1;

    public string GetScene() => sceneToLoad;

    public int GetSceneEntranceNumber() => entranceNumber;

    private void OnDrawGizmos()
    {
        var collider = GetComponent<PolygonCollider2D>();
        if (collider == null) return;

        var points = collider.points;
        Vector2 center = (Vector2)transform.position + collider.offset;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 current = center + points[i];
            Vector2 next = center + points[(i + 1) % points.Length];

            Gizmos.DrawLine(current, next);
        }
    }
}