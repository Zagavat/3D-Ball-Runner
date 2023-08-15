using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawn _spawn;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _startScreen.ExitButtonClick += OnExitButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartBattonClick;
        _gameOverScreen.ExitButtonClick += OnExitButtonClick;
        _player.OnDead += GameOver;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _startScreen.ExitButtonClick -= OnExitButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartBattonClick;
        _gameOverScreen.ExitButtonClick -= OnExitButtonClick;
        _player.OnDead -= GameOver;
    }

    private void Start()
    {
        _spawn.StartSpawn();
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnStartButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnRestartBattonClick()
    {
        _gameOverScreen.Close();
        StartGame();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _player.ResetPlayer();
    }

    private void GameOver()
    {
        _gameOverScreen.Open();
        Time.timeScale = 0;
    }
}
