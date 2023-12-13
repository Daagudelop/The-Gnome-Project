using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
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
    public GameStates currentGameState = GameStates.menu;

    private void Awake()
    {
        if (sharedInstanceManager == null)
        {
            sharedInstanceManager = this;
        }
    }

    public void StartGame()
    {
        setGameState(GameStates.inGame);
    }

    public void BackToMenu()
    {
        setGameState(GameStates.menu);
    }


    public void GameOver()
    {
        setGameState(GameStates.gameOver);
    }

    private void setGameState(GameStates newGameState)
    {
        if (newGameState == GameStates.menu)
        {
            //TODO: colocar la lógica del menu
        }
        else if (newGameState == GameStates.inGame)
        {
            //TODO: colocar la lógica del ingame
        }
        else if (newGameState == GameStates.gameOver)
        {
            //TODO: colocar la lógica del GameOver
        }

        //Crea las instancias.
        this.currentGameState = newGameState;
    }
}
