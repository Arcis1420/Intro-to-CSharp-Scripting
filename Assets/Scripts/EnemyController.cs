using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameEventDispatcher.TriggerEnemyDefeated();
        Destroy(gameObject);
    }

    public void ReloadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }


    [SerializeField] private float patrolDelay = 1;
    [SerializeField] private float patrolSpeed = 3;
    [SerializeField] private int contactDamage = 3;
    [SerializeField] private float knockbackForce = 3f;

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
        var sprite = GetComponent<SpriteRenderer>();

        switch (state)
        {
            case GameState.Starting:
                sprite.color = Color.grey;
                break;

            case GameState.Playing:
                sprite.color = new Color(1f, 0f, 218f / 255f);
                break;

            case GameState.Paused:
                sprite.color = Color.gray;
                break;

            case GameState.FailScreen:
                sprite.color = Color.gray;
                break;
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
        if (!other.transform.CompareTag("Player")) return;

        var health = other.transform.GetComponent<HealthSystem>();
        var player = other.transform.GetComponent<PlayerController>();

        if (health != null)
        {
            health.Damage(contactDamage);
        }

        if (player != null)
        {
            Vector2 awayDirection = (other.transform.position - transform.position).normalized;
            player.Recoil(awayDirection * knockbackForce);
        }
    }






}