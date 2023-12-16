using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LogicQuality : MonoBehaviour
{
    public TMP_Dropdown _qualityDropdown;
    public int _quality;
    // Start is called before the first frame update
    void Start()
    {
        _quality = PlayerPrefs.GetInt("numberQuality", 4);
        _qualityDropdown.value = _quality;
        ChangeQuality();
    }
    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(_qualityDropdown.value);
        PlayerPrefs.SetInt("numberQuality", _qualityDropdown.value);
        _quality = _qualityDropdown.value;
    }
}
