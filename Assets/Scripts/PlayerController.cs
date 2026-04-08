using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem; //Don't miss this!

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _ballPrefab;
    private PlayerInput _input;
    private Rigidbody2D _rigidbody;

    //*NEW* remember facing direction (even after stopped)
    private Vector2 _facingVector = Vector2.right;


 

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

        //BEGIN NEW CODE
        if (_input.actions["Pause"].WasPressedThisFrame())
        {
            GameManager.Instance.TogglePause();
        }
        //END NEW CODE


        //if Fire action was performed log it to the console
        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            //create a new object that is a clone of the ballPrefab
            //at this object's position and default rotation
            //and use a new variable (ball) to reference the clone
            var ball = Instantiate(_ballPrefab,
                                transform.position,
                                Quaternion.identity);
            //Get the Rigidbody 2D component from the new ball 
            //and set its velocity to x:-10f, y:0, z:0
            ball.GetComponent<BallController>()?.SetDirection(_facingVector);

            if (GameManager.Instance.State != GameState.Playing) return;

        }
    }

    private void FixedUpdate()
    {
        var dir = _input.actions["Move"].ReadValue<Vector2>();
        _rigidbody.velocity = dir * 5;

        //to keep track of facing for aiming when stopped:
        //Set _facingVector only while controls are moving
        //(digital only gives 8 directions, analog is nicer)
        if (dir.magnitude > 0.5)
        {
            _facingVector = _rigidbody.velocity;
        }

        if (GameManager.Instance.State != GameState.Playing) return;

    }
}

