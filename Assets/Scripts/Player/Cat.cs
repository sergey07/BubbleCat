using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void ResurrectExtern();
#endif

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

    public void ShowAdvButton()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        ResurrectExtern();
#endif
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
            // TODO: show panel with button Resurrect (the event click binds with Cat.ShowAdvButton())
            // and StartGame (the event click binds with GameManager.LoadFirstLevel())
        }
    }
}
