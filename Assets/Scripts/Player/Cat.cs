using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _catVisual;

    [SerializeField] public AudioClip _audioClipMau;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void LateUpdate()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
        if (collision.gameObject.CompareTag("FalledCatTrigger"))
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InCatDiedScene);
            GameManager.Instance.LoadCatDiedScene();
        }
        else if (collision.gameObject.CompareTag("Boiler"))
        {
            SceneManager.LoadScene(GameProgress.currentSceneName);
        }
    }
}
