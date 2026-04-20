using JetBrains.Annotations;
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


    public void AcceptDefeat()
    {
        Destroy(gameObject);
    }



    [SerializeField] private float patrolDelay = 1;
    [SerializeField] private float patrolSpeed = 3;

    private Rigidbody2D _rb;
    private WaypointPath _waypointPath;
    private Vector2 _patrolTargetPosition;
    private Animator _animator;


    // Awake is called before Start
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _waypointPath = GetComponentInChildren<WaypointPath>();
        _animator = GetComponent<Animator>();
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

        var dir = _patrolTargetPosition - (Vector2)transform.position;

        if (dir.magnitude <= 0.1)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            dir = _patrolTargetPosition - (Vector2)transform.position;
        }

        if (GameManager.Instance.State == GameState.Playing)
        {
            _rb.velocity = dir.normalized * patrolSpeed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

    }

    public void TakeHit()
    {
        _animator.Play(stateName: "EnemyHit");
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<HealthSystem>()?.Damage(3);
            Vector2 awayDirection = (Vector2)(other.transform.position - transform.position);
            other.transform.GetComponent<PlayerController>()?.Recoil(awayDirection * 3f);
        }
    }


    

   

}