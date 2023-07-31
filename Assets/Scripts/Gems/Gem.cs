using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collectEffect;

    public void StartCollectEffect()
    {
        var effect = Instantiate(_collectEffect);
        effect.transform.parent = null;
        effect.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(effect.gameObject, 0.5f);
    }
}
