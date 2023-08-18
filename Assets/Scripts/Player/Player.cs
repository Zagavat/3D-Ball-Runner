using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMove _moving;
    [SerializeField] private int _livesOnStart;

    private int _collectedGems = 0;
    private int _lives;

    public event UnityAction<int> GemsCountChanged;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Dead;

    private void OnEnable()
    {
        _moving.GemCollected += OnGemCollected;
        _moving.ShitHappened += OnShitHappened;
    }

    private void OnDisable()
    {
        _moving.GemCollected -= OnGemCollected;
        _moving.ShitHappened -= OnShitHappened;
    }

    private void Start()
    {
        ResetPlayer();
    }

    private void OnGemCollected(Vector3 crutch)
    {
        _collectedGems++;
        GemsCountChanged?.Invoke(_collectedGems);
    }

    private void OnShitHappened()
    {
        _lives--;
        HealthChanged?.Invoke(_lives);

        if (_lives == 0)
        {
            Dead?.Invoke();
        }
    }

    public void ResetPlayer()
    {
        _collectedGems = 0;
        _lives = _livesOnStart;
        GemsCountChanged?.Invoke(_collectedGems);
        HealthChanged?.Invoke(_lives);
    }
}
