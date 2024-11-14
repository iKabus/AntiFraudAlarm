using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistation;
    private void Update()
    {
        float xCoordinate = Mathf.PingPong(Time.time * _speed, _maxDistation);
        
        transform.position = new Vector2(xCoordinate, transform.position.y);
    }
}