using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGlobalSettings : MonoBehaviour
{
    private void Awake()
    {
        var _dontDestroyBetweenScenes = FindObjectsOfType<LogicGlobalSettings>();

        if(_dontDestroyBetweenScenes.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
