using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    
    private Coroutine _coroutine;
    private bool _isTriggered = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Mover>(out _) && _isTriggered == false)
        {
            _coroutine = StartCoroutine(PlaingSound());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Mover>(out _))
        {
            _isTriggered = true;
        }    
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator PlaingSound()
    {
        float wait = 1f;
        float volume = 0;
        float changeValue = 30f;

        while (enabled)
        {
            if (_isTriggered == false)
            {
                volume = Mathf.MoveTowards(volume, 1, changeValue * Time.deltaTime);

                if (!_sound.isPlaying)
                {
                    _sound.Play();
                }

                _sound.volume = volume;
            }
            else if (_isTriggered == true)
            {
                volume = Mathf.MoveTowards(volume, 0, changeValue * Time.deltaTime);
                _sound.volume = volume;
                
                if (volume == 0)
                {
                    StopCoroutine(_coroutine);

                    _isTriggered = false;
                }
            }
            
            yield return new WaitForSeconds(wait);
        }
    }
}