using UnityEngine;

public class FPSCameraControler : MonoBehaviour
{
    private float _pitch = 0, _yaw = 0;
    private Rigidbody _rigidbody;
    private GameControls _gameControls;

    private Vector2 GetMouseDelta => _gameControls.CameraControls.MouseDelta.ReadValue<Vector2>();

    private void Start()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
        _gameControls = InputManager._gameControls;
        _gameControls.CameraControls.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _yaw += GetMouseDelta.x * InputManager.XSensitivity * Time.deltaTime;
        _pitch -= GetMouseDelta.y * InputManager.XSensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90, 90);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
        _rigidbody.transform.rotation = Quaternion.Euler(0 , _yaw, 0);
    }
}
