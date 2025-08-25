using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerStatus { InStartGameScene, InGame, BubbleBurst, InCatDiedScene, InFinishGameScene }

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Speed Configuration")]
    [SerializeField] private float _horizontalSpeed = 5.0f;
    [SerializeField] private float _maxYSpeed = 20.0f;
    [SerializeField] private float _ySpeedMultiplayer = 2f;
    [SerializeField] private float _fallingSpeed = 5.0f;

    [Header("Game Objects")]
    [SerializeField] private Cat _cat;
    [SerializeField] private Bubble _bubble;

    [Space]
    [SerializeField] private PlayerStatus _playerStatus = PlayerStatus.InGame;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;

    private GameObject _spawnPoint;

    private Vector2 _inputVector;
    private bool _isFinish;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _isFinish = false;
        _bubble.InitBubble();
    }

    private void Update()
    {
        if (_playerStatus == PlayerStatus.InGame)
        {
            _inputVector = GameInput.Instance.GetMovementVector();

            if (Input.GetKeyDown(KeyCode.Y))
            {
                LevelManager.Instance.RestartLevel();
            }
        }

        _cat.UpdateCat();
        _bubble.UpdateBubble();
    }

    private void FixedUpdate()
    {
        switch (_playerStatus)
        {
            case PlayerStatus.InGame:
                if (!_isFinish)
                {
                    HandleInput();
                }
                break;
            case PlayerStatus.BubbleBurst:
            case PlayerStatus.InCatDiedScene:
                _rb.MovePosition(_rb.position - new Vector2(0, _fallingSpeed * Time.fixedDeltaTime));
                break;
        }
    }

    public void Spawn(GameObject currentLevelGO)
    {
        _spawnPoint = currentLevelGO.transform.Find("SpawnPoint").gameObject;
        transform.position = _spawnPoint.transform.position;
        Reset();
    }

    public void ResetPhisic()
    {
        _rb.isKinematic = true;
        _rb.isKinematic = false;
    }

    public void Freeze()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }

    public void Unfreeze()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _isFinish = false;
    }

    public bool IsFinish()
    {
        return _isFinish;
    }

    public void SetFinish(bool isFinish)
    {
        _isFinish = isFinish;
        Freeze();
    }

    public void Reset()
    {
        gameObject.SetActive(true);

        if (_bubble != null)
        {
            _bubble.ResetScale();
        }

        Unfreeze();
    }

    public PlayerStatus GetPlayerStatus()
    {
        return _playerStatus;
    }

    public void SetPlayerStatus(PlayerStatus status)
    {
        _playerStatus = status;

        if (_playerStatus == PlayerStatus.BubbleBurst || _playerStatus == PlayerStatus.InCatDiedScene)
        {
            _cat.SetFalling(true);
        }
        else
        {
            _cat.SetFalling(false);
        }
    }

    public void Fall()
    {
        _cat.transform.position = _bubble.transform.position;
        _bubble.gameObject.SetActive(false);
        _cat.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

        GameObject.Find("CollideAble").gameObject.GetComponent<TilemapCollider2D>().enabled = false;

        _bubble.PlaySound();
        _cat.PlaySound();
        _bubble.Boom();

        SetPlayerStatus(PlayerStatus.BubbleBurst);
    }

    private void HandleInput()
    {
        if (_rb == null)
        {
            return;
        }

        float deltaScale = _bubble.GetDeltaScale();
        float velocityY = deltaScale * _ySpeedMultiplayer;
        velocityY = Mathf.Clamp(velocityY, -_maxYSpeed, _maxYSpeed);

        float newPosX = _inputVector.x * (_horizontalSpeed * Time.fixedDeltaTime);
        float newPosY = velocityY * Time.fixedDeltaTime; 

        _rb.MovePosition(_rb.position + new Vector2(newPosX, newPosY));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Fall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Witch") && !_isFinish)
        {
            Fall();
        }
    }
}
