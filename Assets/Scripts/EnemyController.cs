using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyController : MonoBehaviour
{

    private void OnEnable()
    {
        GameManager.OnAfterStateChanged += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnAfterStateChanged -= HandleGameStateChange;
    }



    [SerializeField] private float patrolDelay = 1;
    [SerializeField] private float patrolSpeed = 3;

    private Rigidbody2D _rb;
    private WaypointPath _waypointPath;
    private Vector2 _patrolTargetPosition;


    // Awake is called before Start
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _waypointPath = GetComponentInChildren<WaypointPath>();
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        if (_waypointPath)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();
        }
        yield return null;
    }

    private void FixedUpdate()
    {
        if (!_waypointPath) return;

        //set our direction toward that waypoint:
        //subtracting our position from target position
        //gives us the slope line between the two
        //We can get direction by normalizing it
        //We can get distance by magnitude
        var dir = _patrolTargetPosition - (Vector2)transform.position;

        //if we are close enough to the target,
        //time to get the next waypoint
        if (dir.magnitude <= 0.1)
        {
            //get next waypoint
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            //change direction
            dir = _patrolTargetPosition - (Vector2)transform.position;
        }

        //this if/else is not in the video (it was made in the GameManager videos)
        //Be sure to update the line in the if clause to match the change in the
        //video instead of adding it above
        if (GameManager.Instance.State == GameState.Playing)
        {
            //UPDATE: how velocity is set
            //normalized reduces dir magnitude to 1, so we can
            //keep at the speed we want by multiplying
            _rb.velocity = dir.normalized * patrolSpeed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    
    private void HandleGameStateChange(GameState state)
    {
        if (state == GameState.Starting)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
        if (state == GameState.Playing)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 218f / 255f);
        }
    }
}