using UnityEngine;

public enum CatSprite { Normal, Flying }

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    [Header("Sprites")]
    // The cat's sprite in normal state
    [SerializeField] private Sprite _normalCat;
    // The falling cat's sprite
    [SerializeField] private Sprite _fallingCat;

    [Header("Sound Configuration")]
    [SerializeField] private AudioClip _audioClipMau;

    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;

    public void UpdateCat()
    {
        PlayerStatus playerStatus = Player.Instance.GetPlayerStatus();

        if (playerStatus == PlayerStatus.InGame)
        {
            HandleInput();
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
                _spriteRenderer.sprite = _fallingCat;
                break;
            case CatSprite.Normal:
            default:
                _spriteRenderer.sprite = _normalCat;
                break;
        }
    }

    public void PlaySound()
    {
        _audioSource.PlayOneShot(_audioClipMau);
    }

    private void HandleInput()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        if (inputVector.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (inputVector.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FalledCatTrigger") && Player.Instance.GetPlayerStatus() != PlayerStatus.InCatDiedScene)
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InCatDiedScene);
            LevelManager.Instance.LoadCatDiedScene();
        }
        else if (collision.gameObject.CompareTag("Boiler"))
        {
            GameObject yandexAdv = GameObject.FindGameObjectWithTag("YandexAdv");
            yandexAdv.GetComponent<YandexAdv>().ShowResurrectPanel();
        }
    }
}
