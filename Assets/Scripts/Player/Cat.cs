using UnityEngine;
//using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _catVisual;

    [Header("Sound Configuration")]
    [SerializeField] public AudioClip _audioClipMau;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetFalling(bool isFalling)
    {
        if (isFalling)
        {
            _catVisual.GetComponent<CatVisual>().SetCatSprite(CatSprite.Flying);
        }
        else
        {
            _catVisual.GetComponent<CatVisual>().SetCatSprite(CatSprite.Normal);
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
