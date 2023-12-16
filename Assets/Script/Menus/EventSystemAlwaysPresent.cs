using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemAlwaysPresent : MonoBehaviour
{
    private void Awake()
    {
        var _dontDestroyBetweenScenesEventSystem = FindObjectsOfType<EventSystemAlwaysPresent>();

        if (_dontDestroyBetweenScenesEventSystem.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
