using UnityEngine;

public enum CatSprite { Normal, Flying }

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    // The cat's sprite in normal state
    [SerializeField] private Sprite _normalCat;
    // The falling cat's sprite
    [SerializeField] private Sprite _fallingCat;

    [Header("Sound Configuration")]
    [SerializeField] public AudioClip _audioClipMau;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayerStatus playerStatus = Player.Instance.GetPlayerStatus();

        if (playerStatus == PlayerStatus.InGame)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        if (inputVector.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (inputVector.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void SetFalling(bool isFalling)
    {
        if (isFalling)
        {
            SetCatSprite(CatSprite.Flying);
        }
        else
        {
            SetCatSprite(CatSprite.Normal);
        }
    }

    public void SetCatSprite(CatSprite catSprite)
    {
        switch (catSprite)
        {
            case CatSprite.Flying:
                GetComponent<SpriteRenderer>().sprite = _fallingCat;
                break;
            case CatSprite.Normal:
            default:
                GetComponent<SpriteRenderer>().sprite = _normalCat;
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FalledCatTrigger") && Player.Instance.GetPlayerStatus() != PlayerStatus.InCatDiedScene)
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InCatDiedScene);
            //GameManager.Instance.LoadCatDiedScene();
            LevelManager.Instance.LoadCatDiedScene();
        }
        else if (collision.gameObject.CompareTag("Boiler"))
        {
            GameObject yandexAdv = GameObject.FindGameObjectWithTag("YandexAdv");
            yandexAdv.GetComponent<YandexAdv>().ShowResurrectPanel();
        }
    }
}
