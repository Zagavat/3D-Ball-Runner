using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Heart _heartTemplate;

    private List<Heart> _hearts = new List<Heart>();

    private void OnEnable()
    {
        _player.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        if(_hearts.Count < value)
        {
            int heartsToCreate = value - _hearts.Count;

            for (int i = 0; i < heartsToCreate; i++)
            {
                CreateHeart();
            }
        }
        else if (_hearts.Count > value)
        {
            int heartsToDelete = _hearts.Count - value;

            for (int i = 0; i < heartsToDelete; i++)
            {
                DestroyHeart(_hearts[_hearts.Count - 1]);
            }
        }
    }

    private void CreateHeart()
    {
        Heart newHeart = Instantiate(_heartTemplate, transform);
        _hearts.Add(newHeart.GetComponent<Heart>());
        newHeart.ToFill();
    }

    private void DestroyHeart(Heart heart)
    {
        _hearts.Remove(heart);
        heart.ToEmpty();
    }
}
