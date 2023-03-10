using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Vars
    public float MovementSpeed = 5;
    public float JumpForce = 3.5f;

    private bool _isGrounded = true;
    private bool _hasJumped = false;
    private Vector3 _movement = Vector3.zero;
    private RaycastHit _groundRayCheck;

    //Refs
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private GameControls _gameControls;

    //Properties
    private bool CarryVelocity => !_isGrounded;

    private Vector3 MoveDirection
    {
        get
        {
            //Converts "Movement" value to a Vector3, swapping the y and z and setting y to 0.
            Vector3 dir = _gameControls.PlayerControls.Movement.ReadValue<Vector2>();
            dir.z = dir.y;
            dir.y = 0;
            return dir.normalized;
        }
    }

    //Events
    public UnityEvent<string> onStateChanged = new UnityEvent<string>();

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<CapsuleCollider>();    
        _gameControls = InputManager._gameControls;
        _gameControls.PlayerControls.Enable();
    }

    private void FixedUpdate()
    {
        PreformChecks();
        Move();
        Jump();
        ApplyVelocity();
    }

    private void PreformChecks() 
    {
        Physics.Raycast(_rigidbody.transform.position, Vector3.down, out _groundRayCheck);

        if (_groundRayCheck.distance <= (_collider.height / 2) + 0.125f) _isGrounded = true;
        else _isGrounded = false;
    }

    private void Move() 
    {
        _movement = (transform.right * MoveDirection.x + transform.forward * MoveDirection.z) * MovementSpeed * (CarryVelocity ? 0.5f : 1);

        if (CarryVelocity) 
        {
            //God if i dont comment this I'll regret it.
            //Since the previous velocity is carried over, so is the previous movement. The follwing is to keep velocity under control so that it doesnt speed up crazily.

            //Keep in mind this is with relativity to direction; when a value is "below" another value, that is reffering to their absolute value.
            //For example if the "velocity value" is -3 and the "move value" is -5, then the velocity value the velocity value is below the move value, despite it being greater than.
            //The objective is that if the velocity value is under the move value, it returns that difference to be added to velocity later.
            //If the velocity value is above the move value, then it returns 0, to prevent additional movement.
            //This additionally clamps the value between 0 and the move value (depending on which is larger) so that movement doesnt surpass the move speed.
            _movement.x = Mathf.Clamp(_movement.x - _rigidbody.velocity.x, _movement.x > 0 ? 0 : _movement.x, _movement.x > 0 ? _movement.x : 0);
            _movement.z = Mathf.Clamp(_movement.z - _rigidbody.velocity.z, _movement.z > 0 ? 0 : _movement.z, _movement.z > 0 ? _movement.z : 0);
        }
    }

    private void Jump() 
    {
        if (!_isGrounded) return;
        if (_gameControls.PlayerControls.Jump.IsPressed() && !_hasJumped)
        {
            _hasJumped = true;
            _movement += Vector3.up * JumpForce;
        }
        else _hasJumped = false;
    }

    private void ApplyVelocity()
    {
        if (CarryVelocity) _rigidbody.velocity += _movement;
        else _rigidbody.velocity = _movement + Vector3.up * _rigidbody.velocity.y;
    }
}
