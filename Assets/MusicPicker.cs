using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPicker : MonoBehaviour
{
    public AudioSource _musicPlayer;
    public AudioClip[] _audioClips;
    private string _currentScene;
    public int _audioRepeatedTimes = 0;
    // Start is called before the first frame update
    void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentScene != "MainMenu" && _currentScene != "Cutscene")
        {
            if (_audioRepeatedTimes < 7 && _musicPlayer.isPlaying == false)
            {
                print(_audioRepeatedTimes);
                _musicPlayer.Play();
                _audioRepeatedTimes += 1;
            }
            else if (_musicPlayer.isPlaying == false)
            {
                _audioRepeatedTimes = 0;
                _musicPlayer.clip = _audioClips[Random.Range(1, _audioClips.Length)];
                _musicPlayer.Play();
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
        _currentScene = scene.name;
        if(_currentScene == "MainMenu")
        {
            _musicPlayer.loop = true;
            _audioRepeatedTimes = 0;
            _musicPlayer.clip = _audioClips[0];
            _musicPlayer.Play();
        }
        else if (_currentScene == "Cutscene")
        {
            _musicPlayer.Stop();
        }
        else
        {
            _musicPlayer.loop = false;
            _audioRepeatedTimes = 0;
            _musicPlayer.clip = _audioClips[Random.Range(1, _audioClips.Length)];
            _musicPlayer.Play();
        }
    }
}
