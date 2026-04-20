
using System;
using UnityEngine;
using UnityEngine.InputSystem; //Don't miss this!

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input; //field to reference Player Input component
    private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _ballPrefab;
    private Vector2 _facingVector = Vector2.right;
    private bool _isRecoiling = false;

    // Start is called before the first frame update
    void Start()
    {
        //set reference to PlayerInput component on this object
        //Top Action Map, "Player" should be active by default
        _input = GetComponent<PlayerInput>();
        //You can switch Action Maps using _input.SwitchCurrentActionMap("UI");

        //set reference to Rigidbody2D component on this object
        _rigidbody = GetComponent<Rigidbody2D>();

        //transform.position = new Vector2(3, -1);
        //Invoke(nameof(AcceptDefeat), 10);
    }

    void AcceptDefeat()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (_input.actions["Pause"].WasPerformedThisFrame())
        {
            GameManager.Instance.TogglePause();
        }

        if (GameManager.Instance.State != GameState.Playing) return;

        //if Fire action was performed log it to the console
        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            var ball = Instantiate(_ballPrefab,
                               transform.position,
                               Quaternion.identity);
            //Get the Rigidbody 2D component from the new ball 
            ball.GetComponent<BallController>()?.SetDirection(_facingVector);
        }
    }

    private void FixedUpdate()
    {

        if (_isRecoiling) return;


        if (GameManager.Instance.State != GameState.Playing) return;

        var dir = _input.actions["Move"].ReadValue<Vector2>();
        _rigidbody.velocity = dir * 5;

        //to keep track of facing for aiming when stopped:
        //Set _facingVector only while controls are moving
        //(digital only gives 8 directions, analog is nicer)
        if (dir.magnitude > 0.5)
        {
            _facingVector = _rigidbody.velocity;
        }
    }

    public void Recoil(Vector2 directionVector)
    {
        _rigidbody.AddForce(directionVector, ForceMode2D.Impulse);
        _isRecoiling = true;
        Invoke(nameof(StopRecoiling), .3f);
    }

    private void StopRecoiling()
    {
        _isRecoiling = false;
    }

}

