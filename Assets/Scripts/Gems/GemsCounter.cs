using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemsCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMove _playerMove;

    private string _scoreLabel;

    public TMP_Text gemsCount;

    private void OnEnable()
    {
        _playerMove.GemCollected += Increase;
    }

    private void OnDisable()
    {
        _playerMove.GemCollected -= Increase;
    }

    private void Start()
    {
        _scoreLabel = gemsCount.text;
    }

    private void Increase()
    {
        gemsCount.text = _scoreLabel + _player.GetGemsCount().ToString();
    }
}
