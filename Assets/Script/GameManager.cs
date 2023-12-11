using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{
    //Instancia compartido, se comporta como una clase estatica, si necesitan algo del manager GameManager.sharedInstanceManager.(lo que necesiten)
    public static GameManager sharedInstanceManager;

    //establecera el estado en el que este el juego actualmente
    public GameState currentGameState = GameState.menu;

    private void Awake()
    {
        if (sharedInstanceManager == null)
        {
            sharedInstanceManager = this;
        }
    }

    public void StartGame()
    {
        setGameState(GameState.inGame);
    }

    public void BackToMenu()
    {
        setGameState(GameState.menu);
    }


    public void GameOver()
    {
        setGameState(GameState.gameOver);
    }

    private void setGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            //TODO: colocar la lógica del menu
        }
        else if (newGameState == GameState.inGame)
        {
            //TODO: colocar la lógica del ingame
        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO: colocar la lógica del GameOver
        }

        //Crea las instancias.
        this.currentGameState = newGameState;
    }
}
