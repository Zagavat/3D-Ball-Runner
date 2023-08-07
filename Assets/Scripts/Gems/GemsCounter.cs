using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemsCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _gemsCount;


    private string _scoreLabel = "";

    private void OnEnable()
    {
        _player.OnGemsCountChanged += ChangeGemsCount;
    }

    private void OnDisable()
    {
        _player.OnGemsCountChanged -= ChangeGemsCount;
    }

    private void Awake()
    {
        _scoreLabel = _gemsCount.text;
    }

    private void ChangeGemsCount(int value)
    {
        _gemsCount.text = _scoreLabel + value.ToString();
    }
}
