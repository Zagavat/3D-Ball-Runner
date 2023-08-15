using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class CameraPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private MusicBar _musicBar;

    private AudioSource _audioSource;
    private Vector3 _offset;

    private void OnEnable()
    {
        _musicBar.MusicValueChanged += OnMusicVolumeChanged;
    }

    private void OnDisable()
    {
        _musicBar.MusicValueChanged += OnMusicVolumeChanged;
    }

    private void Start()
    {
        _offset = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMusicVolumeChanged(float volume)
    {
        _audioSource.volume = volume;
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
