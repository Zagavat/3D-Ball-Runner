using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMove _moving;

    private int _collectedGems = 0;

    private void OnEnable()
    {
        _moving.GemCollected += IncreaseCollectedGems;
    }

    private void OnDisable()
    {
        _moving.GemCollected -= IncreaseCollectedGems;
    }

    private void IncreaseCollectedGems()
    {
        _collectedGems++;
    }

    public int GetGemsCount()
    {
        return _collectedGems;
    }

    public void ResetPlayer()
    {
        _collectedGems = 0;
    }
}
