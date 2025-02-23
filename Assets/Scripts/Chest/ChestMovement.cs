using System.Threading;
using UnityEngine;

public class ChestMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _rate = 1f;

    private bool _isDirectionUp = true;
    private float _timer = 0;

    private void Update()
    {
        float posY = transform.position.y;
        float deltaTime = Time.deltaTime;
        float offsetY = _speed * deltaTime;

        _timer += deltaTime;

        if (_timer >= _rate)
        {
            _isDirectionUp = !_isDirectionUp;
            _timer = 0;
        }

        transform.Translate(0, _isDirectionUp ? offsetY : -offsetY, 0);
    }
}
