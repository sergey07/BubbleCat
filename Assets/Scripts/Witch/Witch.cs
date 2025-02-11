using UnityEngine;

public enum WitchMoveDirection
{
    Left,
    Right,
    Up,
    Down
}

[SelectionBase]
public class Witch : MonoBehaviour
{
    [SerializeField] private WitchMoveDirection _startMoveDirection;
    [SerializeField] private float _speed;

    [Header("Sound Configuration")]
    [SerializeField] private AudioClip _audioClipLaughter;
    [SerializeField] private float _delay = 0;
    [SerializeField] private float _repeatRate = 5;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private Vector2 _movementVector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InvokeRepeating("PlayLaughter", _delay, _repeatRate);

        _movementVector = new Vector2(0, 0);

        switch (_startMoveDirection)
        {
            case WitchMoveDirection.Left:
                _movementVector.x = -1;
                break;
            case WitchMoveDirection.Right:
                _movementVector.x = 1;
                break;
            case WitchMoveDirection.Up:
                _movementVector.y = 1;
                break;
            case WitchMoveDirection.Down:
                _movementVector.y = -1;
                break;
            default:
                break;
        }
    }

    private void PlayLaughter()
    {
        _audioSource.PlayOneShot(_audioClipLaughter);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (_movementVector * _speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (_startMoveDirection == WitchMoveDirection.Left || _startMoveDirection == WitchMoveDirection.Right)
            {
                _movementVector.x *= -1;
            }
            else if (_startMoveDirection == WitchMoveDirection.Up || _startMoveDirection == WitchMoveDirection.Down)
            {
                _movementVector.y *= -1;
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Witch stops when collides with the player
            _speed = 0;
        }
    }
}
