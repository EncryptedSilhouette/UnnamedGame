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

        _xSensitivityInputField.onEndEdit.AddListener((input) => 
        {
            Debug.Log("editedText");
            if (float.TryParse(input, out float val)) 
            {
                InputManager.XSensitivity = val;
                _xSensitivityInputField.SetTextWithoutNotify(InputManager.XSensitivity.ToString());
            }
            else _xSensitivityInputField.SetTextWithoutNotify(PlayerPrefs.HasKey("x_sensitivity") ? PlayerPrefs.GetFloat("x_sensitivity").ToString() : 1.00f.ToString());
        });
        _ySensitivityInputField.onEndEdit.AddListener((input) => 
        {
            Debug.Log("editedText");
            if (float.TryParse(input, out float val))
            {
                InputManager.YSensitivity = val;
                _ySensitivityInputField.SetTextWithoutNotify(InputManager.XSensitivity.ToString());
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
