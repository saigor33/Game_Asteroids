﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManagerController : MonoBehaviour
{
    /// <summary>
    /// Звук разрушения астеройда.
    /// </summary>
    //Сделан в данном месте т.к. при разрушении астеройда, он сразу возвращается в пул и звук отдута не воспроизводиться.
    //Скрывать астеройд не вариат т.к. взаимодействие с коллайдером останется
    public static AudioSource AudioSourceDestroingAsteroid;

    [SerializeField]
    private TextMeshProUGUI _textHealthPoint;
    [SerializeField]
    private TextMeshProUGUI _textSourcePoint;
    [SerializeField]
    private GameObject _panelPause;
    [SerializeField]
    private GameObject _panelGameOver;
    [SerializeField]
    private AudioSource _audioSourceDestroingAsteroidCompomemt;

    private bool _isGameOver;

    private void Awake()
    {
        Time.timeScale = 1;
        GlobalSettings.IsPause = false;

        if (_textHealthPoint == null)
            Debug.LogError($"{this}(_textHealthPoint): не добавлен объект Text Mesh Pro");
        if (_textSourcePoint == null)
            Debug.LogError($"{this}(_textSourcePoint): не добавлен объект Text Mesh Pro");
        if (_panelPause == null)
            Debug.LogError($"{this}(_panelPause): не добавлен объект Panel");
        if (_panelGameOver == null)
            Debug.LogError($"{this}(_panelGameOver): не добавлен объект Panel");
        if (_audioSourceDestroingAsteroidCompomemt == null)
            Debug.LogError($"{this}(_audioSourceDestroingAsteroid): не добавлен объект AudioSource");
        AudioSourceDestroingAsteroid = _audioSourceDestroingAsteroidCompomemt;
    }

    private void Update()
    {
        if (!Input.anyKey)
            return;

        if (Input.GetButtonDown(Data.NAME_BUTTON_PAUSE))
            UseWindowPause();
    }

    /// <summary>
    /// Открытие/закрытие окна паузы игры
    /// </summary>
    private void UseWindowPause()
    {
        if (_isGameOver)
            return;

        if (GlobalSettings.IsPause)
        {
            _panelPause.SetActive(false);
            Time.timeScale = 1;
            GlobalSettings.IsPause = false;
        }
        else
        {
            GlobalSettings.IsPause = true;
            _panelPause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Запуск сценария конца игры
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        ShowPanelGameOver();
    }

    private void ShowPanelGameOver()
    {
        _panelGameOver.SetActive(true);
    }

    public void ButtonResume_onClick()
    {
        UseWindowPause();
    }

    public void ButtonRestart_onClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonExit_onClick()
    {
        Application.Quit();
    }

    /// <summary>
    /// Обновить данные о количестве жизней игрока
    /// </summary>
    /// <param name="countHealth"></param>
    public void UpdateTextForHealthPoint(string countHealth)
    {
        _textHealthPoint.text = countHealth;
    }

    /// <summary>
    /// Обновить кол-во очков, которое имеет игрок
    /// </summary>
    /// <param name="countPoints"></param>
    public void UpdateTextForSourcePoints(string countPoints)
    {
        _textSourcePoint.text = countPoints;
    }

}
