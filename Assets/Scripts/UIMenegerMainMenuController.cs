using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMenegerMainMenuController : MonoBehaviour
{
    /// <summary>
    /// Обработчик нажатия на кнопку "Начать игру"
    /// </summary>
    public void ButtonStartGame_onClick()
    {
        StartGame();
    }

    /// <summary>
    /// Обработчик нажатия на кнопку "Выход"
    /// </summary>
    public void ButtonExit_onClick()
    {
        ExitGame();
    }

    /// <summary>
    /// Запуск сцены с игрой
    /// </summary>
    private void StartGame()
    {
        SceneManager.LoadScene(Data.NAME_SCENCE_GAME);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    private void ExitGame()
    {
        Application.Quit();
    }
}
