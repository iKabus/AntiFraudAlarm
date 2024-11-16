using System;
using System.Collections;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private Alarm _alarm;

    private Coroutine _coroutine;
    
    private float _maxVolume = 1;
    private float _minVolume = 0;
    private float _changeValue = 0.4f;

    private void Awake()
    {
        _sound.volume = 0;
    }

    private void OnEnable()
    {
        _alarm.MoverEnter += PlaySound;
        _alarm.MoverLeave += StopSound;
    }

    private void OnDisable()
    {
        _alarm.MoverEnter -= PlaySound;
        _alarm.MoverLeave -= StopSound;
    }

    private void PlaySound()
    {
        _coroutine = StartCoroutine(PlaingSound(_maxVolume));
    }

    private void StopSound()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(PlaingSound(_minVolume));
    }

    private IEnumerator PlaingSound(float targetVolume)
    {
        float accuracy = 0.00001f;
        
        if (Mathf.Abs(targetVolume - _maxVolume) < accuracy)
        {
            _sound.Play();
        }

        while (Mathf.Abs(_sound.volume - targetVolume) > accuracy)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, targetVolume, _changeValue * Time.deltaTime);
            
            yield return null;
        }

        if (Mathf.Abs(_sound.volume - _minVolume) < accuracy)
        {
            _sound.Stop();
        }
    }
}