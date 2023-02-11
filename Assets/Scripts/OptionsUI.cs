using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField]
    TextMeshPro XValueText;
    [SerializeField]
    Slider XSlider;
    [SerializeField]
    TextMeshPro YValueText;
    [SerializeField]
    Slider YSlider;

    void Start()
    {
        YSlider.onValueChanged.AddListener((x) => XValueText.text = $"X Sensitivity: {x}") ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
