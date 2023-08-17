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

    public event UnityAction<int> OnGemsCountChanged;
    public event UnityAction<int> OnHealthChanged;
    public event UnityAction OnDead;

    private void OnEnable()
    {
        _moving.GemCollected += IncreaseCollectedGems;
        _moving.ShitHappened += LoseLive;
    }

    private void OnDisable()
    {
        _moving.GemCollected -= IncreaseCollectedGems;
        _moving.ShitHappened -= LoseLive;
    }

    private void Start()
    {
        ResetPlayer();
    }

    private void IncreaseCollectedGems(Vector3 crutch)
    {
        _collectedGems++;
        OnGemsCountChanged?.Invoke(_collectedGems);
    }

    private void LoseLive()
    {
        _lives--;
        OnHealthChanged?.Invoke(_lives);

        if (_lives == 0)
        {
            OnDead?.Invoke();
        }
    }

    public void ResetPlayer()
    {
        _collectedGems = 0;
        _lives = _livesOnStart;
        OnGemsCountChanged?.Invoke(_collectedGems);
        OnHealthChanged?.Invoke(_lives);
    }
}
