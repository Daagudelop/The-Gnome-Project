using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogicFullScreen : MonoBehaviour
{
    public Toggle toggle;

    public TMP_Dropdown _resolutionsDropdown;
    Resolution[] _resolutions;
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }


        CheckResolution();
    }


    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void CheckResolution()
    {
        _resolutions = Screen.resolutions;
        _resolutionsDropdown.ClearOptions();
        List<string> _options = new List<string>();
        int _currentResolution = 0;
        

 

        for (int i=0; i<_resolutions.Length; i++)
        {
            /*
            string _res = _resolutions[i].width + " x " + _resolutions[i].height;
            string _nextRes = _resolutions[i+1].width + " x " + _resolutions[i+1].height;
            if (_res == _nextRes)
            {
                goto DontAddRes;
            }
            */

            string _option = _resolutions[i].width + " x " + _resolutions[i].height;
            
            _options.Add(_option);

            if (Screen.fullScreen && _resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResolution = i;
            }

            //DontAddRes: ;
        }
        

       

        _resolutionsDropdown.AddOptions(_options);
        _resolutionsDropdown.value = _currentResolution;
        _resolutionsDropdown.RefreshShownValue();

        _resolutionsDropdown.value = PlayerPrefs.GetInt("_resolutionNumber", 0);
    }

    public void ChangeResolution(int _resolutionIndex)
    {
        PlayerPrefs.SetInt("_resolutionNumber", _resolutionsDropdown.value);

        Resolution _resolution = _resolutions[_resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }
}
