using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectsPool : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerScript;
    [SerializeField] private GameObject _collectEffect;
    [SerializeField] private EffectsBar _effectsBar;
    [SerializeField] private AudioClip _rollSound;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _dieSound;

    private List<GameObject> _collectEffectsPool = new List<GameObject>();
    private int _poolCount = 8;
    private float _soundVolume;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _effectsBar.EffectsValueChanged += ChangeVolume;
        _playerScript.Jump += PlayJumpEffect;
        _playerScript.Rolling += PlayRollingEffect;
        _playerScript.Died += PlayDieEffect;
        _playerScript.GemCollected += PlayCollectEffect;
    }

    private void OnDisable()
    {
        _effectsBar.EffectsValueChanged += ChangeVolume;
        _playerScript.Jump -= PlayJumpEffect;
        _playerScript.Rolling -= PlayRollingEffect;
        _playerScript.Died -= PlayDieEffect;
    }

    private void Start()
    {
        _soundVolume = _effectsBar.GetVolume();
        _audioSource = GetComponent<AudioSource>();
        Initialize();
    }

    private void ChangeVolume(float value)
    {
        _soundVolume = value;
    }

    private void Initialize()
    {
        GameObject instantiated;

        for (int i = 0; i < _poolCount; i++)
        {
            instantiated = Instantiate(_collectEffect);
            instantiated.SetActive(false);
            _collectEffectsPool.Add(instantiated);
        }
    }

    private IEnumerator Playing(float delay, GameObject effect)
    {
        float elapsedTime = 0;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        effect.SetActive(false);
    }

    private void PlayRollingEffect(Vector3 position)
    {
        _audioSource.volume = _soundVolume;
        transform.position = position;

        if (_audioSource.isPlaying != true || _audioSource.clip != _rollSound)
        {
            _audioSource.clip = _rollSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    public void PlayJumpEffect(Vector3 position)
    {
        _audioSource.Stop();
        _audioSource.volume = _soundVolume;
        transform.position = position;
        _audioSource.clip = _jumpSound;
        _audioSource.loop = false;
        _audioSource.Play();
    }

    public void PlayDieEffect(Vector3 position)
    {
        _audioSource.volume = _soundVolume;
        transform.position = position;

        if (_audioSource.clip != _dieSound)
        {
            _audioSource.clip = _dieSound;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }

    private void PlayCollectEffect(Vector3 position)
    {
        var effect = _collectEffectsPool.Where(p => p.activeSelf == false).FirstOrDefault();
        AudioSource audioSource;

        if (effect != null)
        {
            var particleDuration = effect.GetComponentInChildren<ParticleSystem>().main.duration;

            if (effect.TryGetComponent<AudioSource>(out audioSource) == true)
            {
                audioSource.volume = _soundVolume;
                effect.transform.position = position;
                var delay = audioSource.clip.length > particleDuration ? audioSource.clip.length : particleDuration;
                effect.SetActive(true);
                StartCoroutine(Playing(delay, effect));
            }
        }
    }
}
