using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5;

    private Vector2 MoveDirection 
    {
        get 
        {
            if (_gameControls is null) _gameControls = CoreManager.CoreManagerSingleton.GameControls;
            return _gameControls.PlayerControls.Movement.ReadValue<Vector2>().normalized;
        }
    }

    private Vector3 movement = Vector3.zero;

    //Refs
    private Camera _camera;
    private Rigidbody _rigidbody;
    private GameControls _gameControls;

    private void Awake()
    {
        _gameControls = CoreManager.CoreManagerSingleton.GameControls;
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
        ApplyVelocity();
    }

    private void Rotate() 
    {
        _rigidbody.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0); 
    }

    private void Move() 
    {
        movement = (transform.right * MoveDirection.x + transform.forward * MoveDirection.y) * playerSpeed;
        movement.y = 0;
    }

    private void ApplyVelocity() 
    {
        _rigidbody.velocity = new Vector3(movement.x, movement.y + _rigidbody.velocity.y, movement.z);
    }
}
