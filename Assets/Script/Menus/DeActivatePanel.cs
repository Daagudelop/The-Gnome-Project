using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActivatePanel : MonoBehaviour
{

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            gameObject.SetActive(false);
        }
    }
}
