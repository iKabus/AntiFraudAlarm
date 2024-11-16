using System;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public event Action MoverEnter;
    public event Action MoverLeave;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Mover>(out _))
        {
            MoverEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Mover>(out _))
        {
            MoverLeave?.Invoke();
        }    
    }
}