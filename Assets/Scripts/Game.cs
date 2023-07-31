using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Spawn _spawn;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _startScreen.ExitButtonClick += OnExitButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartBattonClick;
        _gameOverScreen.ExitButtonClick += OnExitButtonClick;
        _playerMove.ShitHappened += GameOver;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _startScreen.ExitButtonClick -= OnExitButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartBattonClick;
        _gameOverScreen.ExitButtonClick -= OnExitButtonClick;
        _playerMove.ShitHappened -= GameOver;
    }

    private void Start()
    {
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
        Debug.Log("Гамовер должен закрыться");
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
        //_playerMove.Restart();
        _spawn.StartSpawn();
        Debug.Log("Игра должна перезапуститься");
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }
}
