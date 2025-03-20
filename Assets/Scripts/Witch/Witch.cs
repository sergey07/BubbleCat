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
    [SerializeField] private float _maxSoundDistance = 20.0f;
    [SerializeField] private float _delay = 0;
    [SerializeField] private float _repeatRate = 5;

    [SerializeField] private GameObject _witchVisual;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private Vector2 _movementVector;
    private Transform _playerTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

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
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (distance <= _maxSoundDistance)
        {
            _audioSource.PlayOneShot(_audioClipLaughter);
        }
    }

    private void FixedUpdate()
    {
        if (_movementVector.x > 0)
        {
            _witchVisual.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            _witchVisual.GetComponent<SpriteRenderer>().flipX = false;
        }

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
