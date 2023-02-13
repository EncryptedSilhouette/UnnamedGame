using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vars
    public float MovementSpeed = 5;
    public float JumpForce = 3.5f;

    private bool _isGrounded = true;
    private bool _isStunned = false;
    private bool _canMoveInAir = false;
    private bool _carryVelocity = false;
    private Vector3 _movement = Vector3.zero;

    private Ray _forwardFacingRay;
    private RaycastHit _forwardFacingRayCheck;
    private RaycastHit _groundRayCheck;
    private RaycastHit _forwardGroundRayCheck;

    //Refs
    private Camera _camera;
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private GameControls _gameControls;

    //Properties
    private bool CanMove => (_isGrounded || _canMoveInAir) && !_isStunned;
    private Vector3 MoveDirection
    {
        get
        {
            Vector3 dir = _gameControls.PlayerControls.Movement.ReadValue<Vector2>();
            dir.z = dir.y;
            dir.y = 0;
            return dir.normalized;
        }
    }

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<CapsuleCollider>();    

        _gameControls = InputManager.InputManagerSingleton.GameControls;
        _gameControls.PlayerControls.Enable();
    }

    private void Update()
    {
        PreformChecks();
        Rotate();
        Move();
        Jump();
        ApplyVelocity();
    }

    private void PreformChecks() 
    {
        //_forwardFacingRay = new Ray()

        Physics.Raycast(_forwardFacingRay);
        Physics.Raycast(_rigidbody.transform.position, Vector3.down, out _groundRayCheck);
        Physics.Raycast(_rigidbody.transform.position, MoveDirection, out _forwardGroundRayCheck);

        //Debug.Log(_groundRayCheck.distance);
        if (_groundRayCheck.distance <= _collider.height / 2 + 0.05f)
        {
            _isGrounded = true;
            _carryVelocity = false;
        }
        else
        {
            _isGrounded = false;
            _carryVelocity = true;
        }
    }

    private void Rotate() => _rigidbody.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);

    private void Move() 
    {
        if (!CanMove) 
        {
            _movement = Vector3.zero;
            return;
        } 

        _movement = (transform.right * MoveDirection.x + transform.forward * MoveDirection.z) * MovementSpeed;
        _movement.y = 0;

        if (_carryVelocity && MovementSpeed != 0) _movement *= 1 - Mathf.Min(_rigidbody.velocity.magnitude / MovementSpeed, 1);
    }

    private void Jump() 
    {
        if (_gameControls.PlayerControls.Jump.IsPressed() && _rigidbody.velocity.y <= 0 && _isGrounded)
        {
            _rigidbody.velocity += Vector3.up * JumpForce;
        }
    }

    private void ApplyVelocity()
    {
        if (_carryVelocity) _rigidbody.velocity += _movement;
        else _rigidbody.velocity = _movement + Vector3.up * _rigidbody.velocity.y;
    }
}
