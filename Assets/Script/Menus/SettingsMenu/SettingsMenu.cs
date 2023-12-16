using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public SettingsController _settingsPanel;
    void Start()
    {
        _settingsPanel = GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsController>();
    }

    // Update is called once per frame


    public void showSettings()
    {
        _settingsPanel._settingsScreen.SetActive(true);
    }
    public void supressSettings()
    {
        _settingsPanel._settingsScreen.SetActive(false);
    }
}
