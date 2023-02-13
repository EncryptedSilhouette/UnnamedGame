using Cinemachine;
using UnityEngine;

public class FPSCameraControler : MonoBehaviour
{
    private void Start()
    {
        //WHY TF DO I HAVE TO DO THIS SHIT;
        CinemachinePOV cinemachinePOV = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
        cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.XSensitivity * 0.125f;
        cinemachinePOV.m_VerticalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.YSensitivity * 0.125f;

        InputManager.InputManagerSingleton.onControlsChanged.AddListener(() => 
        {
            cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.XSensitivity * 0.125f;
            cinemachinePOV.m_VerticalAxis.m_MaxSpeed = InputManager.InputManagerSingleton.YSensitivity * 0.125f;
        });

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
