using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public enum PlayerStatus { InStartGameScene, InGame, BubbleBurst, InCatDiedScene, InFinishGameScene }

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    // �������� �� �����������
    [SerializeField] private float _horizontalSpeed = 5.0f;
    // ������������ ������������ ��������
    [SerializeField] private float _maxYSpeed = 20.0f;
    // ��������� ��� ������������ ��������
    [SerializeField] private float _ySpeedMultiplayer = 2f;
    // �������� ������� ����
    [SerializeField] private float _fallingSpeed = 5.0f;

    // ������ ����
    [SerializeField] private GameObject _catObject;
    // ������ ������
    [SerializeField] private GameObject _bubbleObject;
    // �����, ��� ���������� ��� � �����, ��� �� ������ � ����
    //[SerializeField] private Transform _spawnPointInCatDiedScene;

    [SerializeField] private PlayerStatus _playerStatus = PlayerStatus.InGame;

    private Rigidbody2D _rb;
    private Bubble _bubbleComponent;
    private Cat _catComponent;

    private Vector2 _inputVector;

    //private bool _isFalling = false;

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();

        _bubbleComponent = _bubbleObject.gameObject.GetComponent<Bubble>();
        _catComponent = _catObject.gameObject.GetComponent<Cat>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "CatDied")
        {
            SetPlayerStatus(PlayerStatus.InCatDiedScene);
            _catComponent.SetFalling(true);
            _catObject.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            _bubbleObject.gameObject.SetActive(false);
        }
    }

    public PlayerStatus GetPlayerStatus()
    {
        return _playerStatus;
    }

    public void SetPlayerStatus(PlayerStatus status)
    {
        _playerStatus = status;
    }

    private void Update()
    {
        if (_playerStatus == PlayerStatus.InGame)
        {
            _inputVector = GameInput.Instance.GetMovementVector();

            if (Input.GetKeyDown(KeyCode.Y))
            {
                GameManager.Instance.RestartScene();
            }
        }
    }

    private void FixedUpdate()
    {
        switch (_playerStatus)
        {
            case PlayerStatus.InGame:
                HandleInput();
                break;
            case PlayerStatus.BubbleBurst:
            case PlayerStatus.InCatDiedScene:
                _rb.MovePosition(_rb.position - new Vector2(0, _fallingSpeed * Time.fixedDeltaTime));
                break;
        }
    }

    private void HandleInput()
    {
        float deltaScale = _bubbleComponent.GetDeltaScale();
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
            _bubbleObject.gameObject.SetActive(false);
            _catObject.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

            GameObject.Find("CollideAble").gameObject.GetComponent<TilemapCollider2D>().enabled = false;

            AudioSource audioSource = _catObject.gameObject.GetComponent<AudioSource>();

            audioSource.PlayOneShot(_bubbleComponent._audioClipCpock);
            audioSource.PlayOneShot(_catComponent._audioClipMau);

            _bubbleObject.GetComponent<Bubble>().Boom();

            SetPlayerStatus(PlayerStatus.BubbleBurst);
            _catComponent.SetFalling(true);
            //catObject.GetComponent<SpriteRenderer>().sprite.name = false;
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        _bubbleComponent.ResetScale();
    }
}
