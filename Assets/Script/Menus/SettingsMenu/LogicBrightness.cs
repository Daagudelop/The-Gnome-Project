using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicBrightness : MonoBehaviour
{
    public Slider _slider;
    public float _sliderValue;

    public Image _panelBrightness;

    public float blackValue;
    public float whiteValue;

    // Start is called before the first frame update

    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("Brillo", 0.5f);
        _panelBrightness.color = new Color(_panelBrightness.color.r, _panelBrightness.color.g, _panelBrightness.color.b, _sliderValue / 3);
    }

    void Update()
    {
        blackValue = 1 - _sliderValue - 0.5f;
        whiteValue = _sliderValue - 0.5f;

        if (_sliderValue < 0.5f)
        {
            _panelBrightness.color = new Color(0, 0, 0, blackValue);
        }

        if (_sliderValue > 0.5f)
        {
            _panelBrightness.color = new Color(255, 255, 255, whiteValue);
        }
    }

    public void ChangeSlider(float valor)
    {
        _sliderValue = valor;
        PlayerPrefs.SetFloat("Brillo", _sliderValue);
        _panelBrightness.color = new Color(_panelBrightness.color.r, _panelBrightness.color.g, _panelBrightness.color.b, _sliderValue);
    }
}
