using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float JumpForce = 11;

    private Vector2 MoveDirection => _gameControls.PlayerControls.Movement.ReadValue<Vector2>().normalized;

    private bool isGrounded = true;
    private bool carryVelocity = false;
    private Vector3 movement = Vector3.zero;

    //Refs
    private Camera _camera;
    private Rigidbody _rigidbody;
    private GameControls _gameControls;

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();

        _gameControls = InputManager.InputManagerSingleton.GameControls;
        _gameControls.PlayerControls.Enable();
    }

    private void Update()
    {
        carryVelocity = true;
        Rotate();
        Move();
        Jump();
        ApplyVelocity();
    }

    private void Rotate() => _rigidbody.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);

    private void Move() 
    {
        float scale;
        movement = (transform.right * MoveDirection.x + transform.forward * MoveDirection.y) * MovementSpeed;

        if (carryVelocity && MovementSpeed != 0) 
        {
            scale = 1 - _rigidbody.velocity.magnitude / MovementSpeed;
            movement *= scale;
        }
        movement.y = 0;
    }

    private void ApplyVelocity() 
    {
        Debug.Log($"(1) Movement: {movement.magnitude}, Velocity: {_rigidbody.velocity.magnitude}, sum: {movement.magnitude + _rigidbody.velocity.magnitude}");
        if (carryVelocity) _rigidbody.velocity += movement;
        else _rigidbody.velocity = movement + Vector3.up * _rigidbody.velocity.y;
        Debug.Log($"(2) Velocity: {_rigidbody.velocity.magnitude}");
    } 

    private void Jump() 
    {
        RaycastHit hit;
        if (!Physics.Raycast(_rigidbody.transform.position, Vector3.down, out hit, 1.1f))
        {
            isGrounded = false;
            Debug.Log(isGrounded);
            return;
        }

        isGrounded = true;
        if (_gameControls.PlayerControls.Jump.IsPressed() && _rigidbody.velocity.y <= 0)
        {
            //Debug.Log("Jump");
            _rigidbody.velocity += Vector3.up * JumpForce;
        }
    }
}
