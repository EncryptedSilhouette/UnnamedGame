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
                val = Mathf.Clamp((float) Math.Round(val, 2, MidpointRounding.AwayFromZero), 00.01f, 99.99f);
                PlayerPrefs.SetFloat("x_sensitivity", val);
                _xSensitivityInputField.SetTextWithoutNotify(val.ToString());
            }
            else _xSensitivityInputField.SetTextWithoutNotify(PlayerPrefs.HasKey("x_sensitivity") ? PlayerPrefs.GetFloat("x_sensitivity").ToString() : _xSensitivityInputField.text = 1.00f.ToString());
        });
        _ySensitivityInputField.onValueChanged.AddListener((input) => 
        {
            if (float.TryParse(input, out float val))
            {
                val = Mathf.Clamp((float) Math.Round(val, 2, MidpointRounding.AwayFromZero), 00.01f, 99.99f);
                PlayerPrefs.SetFloat("y_sensitivity", val);
                _ySensitivityInputField.SetTextWithoutNotify(val.ToString());
            }
            else _ySensitivityInputField.SetTextWithoutNotify(PlayerPrefs.HasKey("y_sensitivity") ? PlayerPrefs.GetFloat("y_sensitivity").ToString() : _ySensitivityInputField.text = 1.00f.ToString());
        });
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
