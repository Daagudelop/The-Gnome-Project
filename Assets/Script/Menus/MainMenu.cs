using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject _gameOverPanel;
    private void Start()
    {
        _gameOverPanel = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>()._gameOverScreen;


    }
    public void NewGame()
    {
        
        SceneManager.LoadScene("Cutscene"); //main game scene (replace when finished)
        Time.timeScale = 1f;
        _gameOverPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
