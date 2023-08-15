using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public event UnityAction<float> MusicValueChanged;

    public void OnValueChanged()
    {
        MusicValueChanged?.Invoke(_slider.value);
    }
}
