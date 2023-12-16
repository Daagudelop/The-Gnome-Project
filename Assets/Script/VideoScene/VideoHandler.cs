using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public AudioSource _getMusicVolume;
    public VideoPlayer _videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _getMusicVolume = GameObject.FindGameObjectWithTag("Settings").GetComponent<MusicController>()._musicGameObject.GetComponent<AudioSource>();
        _videoPlayer = gameObject.GetComponent<VideoPlayer>();
        _videoPlayer.SetDirectAudioVolume(0, _getMusicVolume.volume);
        _videoPlayer.loopPointReached += EndReached;
    }
    private void FixedUpdate()
    {
        _videoPlayer.SetDirectAudioVolume(0, _getMusicVolume.volume);

    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene"); //main gameplay scene (replace when finished)
        Time.timeScale = 1f;
    }
}
