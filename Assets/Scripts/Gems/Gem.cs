using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    private EffectsPool _effectsPool;

    private void Start()
    {
        _effectsPool = FindObjectOfType<EffectsPool>();
    }

    public void StartCollectEffect()
    {
        _effectsPool.PlayCollectEffect(transform.position);
        gameObject.SetActive(false);
    }
}