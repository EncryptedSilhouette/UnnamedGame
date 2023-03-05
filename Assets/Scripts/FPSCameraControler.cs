using Cinemachine;
using UnityEngine;

public class FPSCameraControler : MonoBehaviour
{
    private float pitch = 0, yaw = 0;

    private Vector2 GetMouseDelta 
    {
        get 
        {
            Vector2 input = _gameControls.CameraControls.MouseDelta.ReadValue<Vector2>();
            return new Vector2(input.x * InputManager.InputManagerSingleton.XSensitivity,
                               input.y * InputManager.InputManagerSingleton.YSensitivity);
        }
    }

    Rigidbody _rigidbody;
    GameControls _gameControls;

    private void Start()
    {
        //WHY TF DO I HAVE TO DO THIS SHIT;
        /*CinemachinePOV cinemachinePOV = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
        cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.XSensitivity * 0.125f;
        cinemachinePOV.m_VerticalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.YSensitivity * 0.125f;

        InputManager.InputManagerSingleton.onControlsChanged.AddListener(() =>
        {
            cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.XSensitivity * 0.125f;
            cinemachinePOV.m_VerticalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.YSensitivity * 0.125f;
        });*/

        _gameControls = InputManager.InputManagerSingleton.GameControls;
        _gameControls.CameraControls.Enable();

        _rigidbody = GetComponentInParent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        pitch = Mathf.Clamp(pitch - GetMouseDelta.y * Time.deltaTime, -90, 90);
        yaw = yaw + GetMouseDelta.x * Time.deltaTime;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        _rigidbody.transform.rotation = Quaternion.Euler(0 , yaw, 0);
    }
}
