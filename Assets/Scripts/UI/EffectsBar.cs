using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EffectsBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public event UnityAction<float> EffectsValueChanged;

    public float GetVolume()
    {
        return _slider.value;
    }

    public void OnValueChanged()
    {
        EffectsValueChanged?.Invoke(_slider.value);
    }
}
