using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject _gameOverScreen;

    private void Awake()
    {
        var _dontDestroyBetweenScenesGameOver = FindObjectsOfType<GameOver>();

        if (_dontDestroyBetweenScenesGameOver.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
