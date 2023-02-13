using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float JumpForce = 11;

    private Vector2 MoveDirection => _gameControls.PlayerControls.Movement.ReadValue<Vector2>().normalized;

    private bool isGrounded = true;
    private Vector3 movement = Vector3.zero;

    //Refs
    private Camera _camera;
    private Rigidbody _rigidbody;
    private GameControls _gameControls;

    private void Awake()
    {
        _gameControls = InputManager.InputManagerSingleton.GameControls;
    }

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _gameControls.PlayerControls.Enable();
    }

    private void Update()
    {
        Rotate();
        Move();
        Jump();
        ApplyVelocity();
    }

    private void Rotate() 
    {
        _rigidbody.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0); 
    }

    private void Move() 
    {
        movement = (transform.right * MoveDirection.x + transform.forward * MoveDirection.y) * JumpForce;
        movement.y = 0;
    }

    private void ApplyVelocity() 
    {
        _rigidbody.velocity = new Vector3(movement.x, movement.y + _rigidbody.velocity.y, movement.z);
    }

    private void Jump() 
    {
        RaycastHit hit;
        if (!Physics.Raycast(_rigidbody.transform.position, Vector3.down, out hit, 1.5f))
        {
            Debug.Log(isGrounded);
            isGrounded = false;
            return;
        }
        if (_gameControls.PlayerControls.Jump.IsPressed() && _rigidbody.velocity.y <= 0)
        {
            Debug.Log("Jump");
            _rigidbody.velocity += Vector3.up * JumpForce;
        }
    }
}
