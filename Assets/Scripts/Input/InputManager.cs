using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager InputManagerSingleton { get; private set; }

    [SerializeField]
    private bool _overrideSavedValue = false;
    [SerializeField]
    private float _xSensitivity = 1;
    [SerializeField]
    private float _ySensitivity = 1;

    private GameControls _gameControls;

    //Events
    public UnityEvent onControlsChanged { get; private set; }

    public GameControls GameControls
    {
        get
        {
            if (_gameControls is null) _gameControls = new GameControls();
            return _gameControls;
        }
        private set 
        {
            _gameControls = value;
            onControlsChanged?.Invoke();
        } 
    }

    public float XSensitivity 
    {
        get => _xSensitivity;
        set 
        {
            _xSensitivity = Mathf.Clamp((float) Math.Round(value, 2, MidpointRounding.AwayFromZero), 00.01f, 99.99f);
            PlayerPrefs.SetFloat("x_sensitivity", _xSensitivity);
            onControlsChanged?.Invoke();
        }
    }

    public float YSensitivity
    {
        get => _xSensitivity;
        set
        {
            _ySensitivity = Mathf.Clamp((float) Math.Round(value, 2, MidpointRounding.AwayFromZero), 00.01f, 99.99f);
            PlayerPrefs.SetFloat("y_sensitivity", _ySensitivity);
            onControlsChanged?.Invoke();
        }
    }

    private void Awake()
    {
        InputManagerSingleton = this;
        onControlsChanged = new UnityEvent();

        if (!PlayerPrefs.HasKey("x_sensitivity") || _overrideSavedValue) XSensitivity = _xSensitivity;
        if (!PlayerPrefs.HasKey("y_sensitivity") || _overrideSavedValue) YSensitivity = _ySensitivity;

        if (_overrideSavedValue) return;
        if (PlayerPrefs.HasKey("x_sensitivity")) XSensitivity = PlayerPrefs.GetFloat("x_sensitivity");
        if (PlayerPrefs.HasKey("y_sensitivity")) YSensitivity = PlayerPrefs.GetFloat("y_sensitivity");
    }

    void Update()
    {
        
    }
}
