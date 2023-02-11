using Cinemachine;
using UnityEngine;

public class FPSCameraControler : MonoBehaviour
{
    [SerializeField]
    private float XSensitivity = 1;
    [SerializeField]
    private float YSensitivity = 1;

    //refs
    GameControls _gameControls;
    CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("x_sens") && PlayerPrefs.HasKey("y_sens")) 
        {
            Debug.Log("err");
            XSensitivity = PlayerPrefs.GetFloat("x_sens");
            YSensitivity = PlayerPrefs.GetFloat("y_sens");
        }
    }

    private void Start()
    {
        //WHY TF DO I HAVE TO DO THIS SHIT;
        CinemachinePOV cinemachinePOV = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
        cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = XSensitivity * 0.125f;
        cinemachinePOV.m_VerticalAxis.m_MaxSpeed = YSensitivity * 0.125f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}