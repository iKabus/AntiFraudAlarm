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
        float waitTime = 1f;
        float currentVolume = 0;
        float minVolume = 0;
        float maxVolume = 1;
        float changeValue = 40f;
        
        var wait = new WaitForSeconds(waitTime);

        while (enabled)
        {
            if (_isTriggered == false)
            {
                if (_sound.isPlaying == false)
                {
                    currentVolume = minVolume;
                    
                    _sound.Play();
                }
                
                currentVolume = Mathf.MoveTowards(currentVolume, maxVolume, changeValue * Time.deltaTime);
                
                _sound.volume = currentVolume;
            }
            else if (_isTriggered == true)
            {
                if (currentVolume == minVolume)
                {
                    StopCoroutine(_coroutine);

                    _isTriggered = false;
                }
                
                currentVolume = Mathf.MoveTowards(currentVolume, minVolume, changeValue * Time.deltaTime);
                _sound.volume = currentVolume;
            }
            
            yield return wait;
        }
    }
}