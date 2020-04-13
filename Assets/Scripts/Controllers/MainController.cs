using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    [SerializeField] private SpawnController spawnCon;
    [SerializeField] private UIController uiCon;

    [SerializeField] private PlayerController playerCon;

    // Идёт ли сейчас игра, перенная паузы
    public bool gameActive = true;

    // Пауза в игре, при смерти или включении опций
    public void GameDeActivate()
    {
        gameActive = false;

        spawnCon.StopGame();
        uiCon.EndGame();
    }
   
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
