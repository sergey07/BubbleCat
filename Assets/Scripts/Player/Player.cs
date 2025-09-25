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
    [SerializeField] private float _finishGameSpeed = 10.0f;

    [Space]
    [SerializeField] private PlayerStatus _playerStatus = PlayerStatus.InGame;

    [Header("Sound Configuration")]
    [SerializeField] private AudioClip _audioClipMeow;
    // The sound of a bubble bursting
    [SerializeField] private AudioClip _audioClipBurst;
    // The gurgling sound of a cat falling into a boiler
    [SerializeField] public AudioClip _audioClipSplash;

    [Header("Components")]
    [SerializeField] private Cat _cat;
    [SerializeField] private Bubble _bubble;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AudioSource _audioSource;

    private GameObject _finishGamePanel;
    private Transform _finishGameEndTriggerTransform;

    private YandexAdv _yandexAdv;
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

        GameObject yandexAdvGO = GameObject.FindGameObjectWithTag("YandexAdv");
        _yandexAdv = yandexAdvGO.GetComponent<YandexAdv>();
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

            if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.Instance.TogglePause();
            }
        }
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
            case PlayerStatus.InFinishGameScene:
                if (_finishGamePanel == null)
                {
                    _finishGamePanel = GameObject.FindGameObjectWithTag("FinishGamePanel");
                    _finishGamePanel.SetActive(false);
                    _finishGameEndTriggerTransform = GameObject.FindGameObjectWithTag("FinishGameEndTrigger").transform;
                }

                _rb.MovePosition(_rb.position + new Vector2(_finishGameSpeed * Time.fixedDeltaTime, 0));
                if (_rb.position.x > _finishGameEndTriggerTransform.position.x)
                {
                    _finishGamePanel.SetActive(true);
                }
                break;
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        _bubble.gameObject.SetActive(true);
        _bubble.ResetScale();
        Unfreeze();
    }

    public void Spawn(GameObject currentLevelGO)
    {
        _spawnPoint = currentLevelGO.transform.Find("SpawnPoint").gameObject;
        transform.position = _spawnPoint.transform.position;

        if (_playerStatus == PlayerStatus.InGame)
        {
            Reset();
        }
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

    public void BubbleBurst()
    {
        _cat.transform.position = _bubble.transform.position;
        _bubble.gameObject.SetActive(false);
        _cat.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

        // Disable collider for enviroment
        GameObject.Find("CollideAble").gameObject.GetComponent<TilemapCollider2D>().enabled = false;

        _audioSource.PlayOneShot(_audioClipBurst);
        _bubble.Burst();
        _audioSource.PlayOneShot(_audioClipMeow);

        SetPlayerStatus(PlayerStatus.BubbleBurst);
    }

    private void HandleInput()
    {
        if (_rb == null)
        {
            return;
        }

        if (_inputVector.x > 0)
        {
            _cat.FlipRight();
        }
        else if (_inputVector.x < 0)
        {
            _cat.FlipLeft();
        }

        if (_inputVector.y > 0)
        {
            _bubble.IncreaseScale();
        }
        else if (_inputVector.y < 0)
        {
            _bubble.DecreaseScale();
        }
        else
        {
            _bubble.ChangeScaleToOrigin();
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
            BubbleBurst();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Witch") && !_isFinish)
        {
            BubbleBurst();
        }
        else if (collision.CompareTag("FalledCatTrigger") && _playerStatus != PlayerStatus.InCatDiedScene)
        {
            _playerStatus = PlayerStatus.InCatDiedScene;
            LevelManager.Instance.LoadCatDiedScene();
        }
        else if (collision.CompareTag("SplashSoundTrigger"))
        {
            _audioSource.PlayOneShot(_audioClipSplash);
        }
        else if (collision.CompareTag("Boiler"))
        {
            _yandexAdv.ShowResurrectPanel();
        }
        else if (!_isFinish && collision.CompareTag("FinishTrigger"))
        {
            _isFinish = true;
            LevelManager.Instance.FinishLevel();
        }
    }
}
