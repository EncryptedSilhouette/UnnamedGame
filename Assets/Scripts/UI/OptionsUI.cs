using System;
using TMPro;
using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _xSensitivityInputField;
    [SerializeField]
    private TMP_InputField _ySensitivityInputField;

    private void Awake()
    {
        _xSensitivityInputField.text = PlayerPrefs.HasKey("x_sensitivity") ? PlayerPrefs.GetFloat("x_sensitivity").ToString() : _xSensitivityInputField.text = 1.00f.ToString();
        _ySensitivityInputField.text = PlayerPrefs.HasKey("y_sensitivity") ? PlayerPrefs.GetFloat("y_sensitivity").ToString() : _ySensitivityInputField.text = 1.00f.ToString();

        _xSensitivityInputField.onValueChanged.AddListener((input) => 
        {
            if (float.TryParse(input, out float val)) 
            {
                InputManager.InputManagerSingleton.XSensitivity = val;
                _xSensitivityInputField.SetTextWithoutNotify(val.ToString());
            }
            else _xSensitivityInputField.SetTextWithoutNotify(PlayerPrefs.HasKey("x_sensitivity") ? PlayerPrefs.GetFloat("x_sensitivity").ToString() : 1.00f.ToString());
        });
        _ySensitivityInputField.onValueChanged.AddListener((input) => 
        {
            if (float.TryParse(input, out float val))
            {
                InputManager.InputManagerSingleton.YSensitivity = val;
                _ySensitivityInputField.SetTextWithoutNotify(val.ToString());
            }
            else _ySensitivityInputField.SetTextWithoutNotify(PlayerPrefs.HasKey("y_sensitivity") ? PlayerPrefs.GetFloat("y_sensitivity").ToString() : 1.00f.ToString());
        });
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
