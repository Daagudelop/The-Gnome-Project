using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public GameObject _pauseMenu;
    public static bool _isPaused;
    public GameObject _settingsPanel;
    public GameObject _gameOverPanel;
    public string _currentScene;
    public VideoPlayer _videoPlayer;
    private void Awake()
    {
        var _dontDestroyBetweenScenesPause = FindObjectsOfType<PauseMenu>();

        if (_dontDestroyBetweenScenesPause.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
        _pauseMenu.SetActive(false);
        _settingsPanel = GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsController>()._settingsScreen;
        _gameOverPanel = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>()._gameOverScreen;

        //I hate myself because of how i had to reference this object, this shit fixes bug of unpausing while being in settings panel


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_settingsPanel.activeSelf && _currentScene!="MainMenu")
        {
            if(_currentScene != "Cutscene")
            {
                if (_isPaused == true)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
            else
            {
                _videoPlayer = GameObject.FindGameObjectWithTag("VideoPlayer").GetComponent<VideoPlayer>();
                if (_isPaused == true)
                {
                    _videoPlayer.Play();
                    ResumeGame();
                }
                else
                {
                    _videoPlayer.Pause();
                    PauseGame();
                }
            }
            
        }
    }
    // Scene detector
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; ;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentScene= scene.name;
        _pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }
    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        if (_currentScene == "Cutscene")
        {
            _videoPlayer = GameObject.FindGameObjectWithTag("VideoPlayer").GetComponent<VideoPlayer>();
            _videoPlayer.Play();
        }
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        _isPaused = false;
        _gameOverPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowSettings()
    {
        _settingsPanel.SetActive(true);
    }

    
     
}
