using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicSFXVolume : MonoBehaviour
{
    public Slider _slider;
    public float _sliderValue;
    public Image _imageMute;
    public Image _image0to24;
    public Image _image25to49;
    public Image _image50to74;
    public Image _image75to100;
    public GameObject _sfxGameObject;
    private AudioSource _sfxAudioSource;

    void Start()
    {

        _sfxAudioSource = _sfxGameObject.GetComponent<AudioSource>();
        _slider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        //AudioListener.volume = _slider.value;

        _sfxAudioSource.volume = _slider.value;


    }

    public void ChangeSliderSFX(float valor)
    {
        _sliderValue = valor;
        PlayerPrefs.GetFloat("musicVolume", _sliderValue);
        //AudioListener.volume = _slider.value;
        _sfxAudioSource.volume = _slider.value;
        //print("sfx: " + _sfxAudioSource.volume);
        CheckMuteSFX();
    }

    public void CheckMuteSFX()
    {
        if (_sliderValue == 0)
        {
            _imageMute.enabled = true;
            _image0to24.enabled = false;
            _image25to49.enabled = false;
            _image50to74.enabled = false;
            _image75to100.enabled = false;
        }
        else if ((0f < _sliderValue) && (_sliderValue <= 0.24f))
        {
            _imageMute.enabled = false;
            _image0to24.enabled = true;
            _image25to49.enabled = false;
            _image50to74.enabled = false;
            _image75to100.enabled = false;
        }
        else if ((0.24f < _sliderValue) && (_sliderValue <= 0.49f))
        {
            _imageMute.enabled = false;
            _image0to24.enabled = false;
            _image25to49.enabled = true;
            _image50to74.enabled = false;
            _image75to100.enabled = false;
        }
        else if ((0.49f < _sliderValue) && (_sliderValue <= 0.74f))
        {
            _imageMute.enabled = false;
            _image0to24.enabled = false;
            _image25to49.enabled = false;
            _image50to74.enabled = true;
            _image75to100.enabled = false;
        }
        else if (0.74f < _sliderValue)
        {
            _imageMute.enabled = false;
            _image0to24.enabled = false;
            _image25to49.enabled = false;
            _image50to74.enabled = false;
            _image75to100.enabled = true;
        }


    }

}
