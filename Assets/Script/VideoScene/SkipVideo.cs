using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipVideo : MonoBehaviour
{
    public void skipVideo()
    {
        SceneManager.LoadScene("IA proves"); //main gameplay scene (replace when finished)
        Time.timeScale = 1f;
    }
}
