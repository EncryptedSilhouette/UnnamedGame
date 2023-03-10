using System;
using UnityEngine;
using UnityEngine.Events;

static class InputManager
{
    private static float _xSensitivity = 1;
    private static float _ySensitivity = 1;

    public static GameControls _gameControls { get; private set; }
    public static UnityEvent _onControlsChanged { get; private set; }

    static InputManager() 
    {
        _xSensitivity = PlayerPrefs.GetFloat("x_sensitivity");
        _ySensitivity = PlayerPrefs.GetFloat("y_sensitivity");

        _gameControls = new();
        _onControlsChanged = new();
        _onControlsChanged.AddListener(() => PlayerPrefs.SetFloat("x_sensitivity", _xSensitivity));
        _onControlsChanged.AddListener(() => PlayerPrefs.SetFloat("y_sensitivity", _ySensitivity));
    }

    public static float XSensitivity 
    {
        get => _xSensitivity;
        set 
        {
            _xSensitivity = Mathf.Clamp((float) Math.Round(value, 2, MidpointRounding.AwayFromZero), 00.01f, 99.99f);
            _onControlsChanged?.Invoke();
        }
    }

    public static float YSensitivity
    {
        get => _ySensitivity;
        set
        {
            _ySensitivity = Mathf.Clamp((float) Math.Round(value, 2, MidpointRounding.AwayFromZero), 00.01f, 99.99f);
            _onControlsChanged?.Invoke();
        }
    }
}
