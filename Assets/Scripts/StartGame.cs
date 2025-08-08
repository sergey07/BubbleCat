using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [Header("Time Configuration")]
    [SerializeField] private float _timeBeforeTranslateCatByWitch = 2.0f;
    [SerializeField] private float _timeBeforeBoilerBoils = 2.0f;
    [SerializeField] private float _timeBeforeBubbleHasCat = 2.0f;
    [SerializeField] private float _timeBeforeLoadFirstLevel = 1.0f;

    [Header("Size Configuration")]
    [SerializeField] private float _catLocalScale = 1.0f;

    [Space]
    [SerializeField] private Animator _animator;

    [Header("Game Objects")]
    [SerializeField] private GameObject _witchObject;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _catObject;
    [SerializeField] private GameObject _bubbleObject;
    [SerializeField] private GameObject _bubbleBoomObject;

    [Space]
    [SerializeField] private Transform _spawnPoint;

    [Header("Sprites")]
    [SerializeField] private Sprite _witchWithCat;
    [SerializeField] private Sprite _witchWithoutCat;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.SetPlayerStatus(PlayerStatus.InStartGameScene);

        _playerObject.SetActive(false);
        _catObject.transform.localScale = new Vector3(_catLocalScale, _catLocalScale, _catLocalScale);
        _playerObject.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);

        _playerObject.transform.position = _spawnPoint.position;

        StartCoroutine(TranslateCatByWitch());
    }

    IEnumerator TranslateCatByWitch()
    {
        yield return new WaitForSeconds(_timeBeforeTranslateCatByWitch);

        _witchObject.transform.localScale = new Vector3(-_witchObject.transform.localScale.x, _witchObject.transform.localScale.y, _witchObject.transform.localScale.z);

        _animator.SetTrigger("boom");

        StartCoroutine(BoilerBoils());
    }

    IEnumerator BoilerBoils()
    {
        yield return new WaitForSeconds(_timeBeforeBoilerBoils);

        _witchObject.GetComponent<SpriteRenderer>().sprite = _witchWithoutCat;
        _playerObject.SetActive(true);
        _catObject.SetActive(false);
        _bubbleObject.SetActive(false);
        _bubbleBoomObject.SetActive(true);

        StartCoroutine(BubbleHasCat());
    }

    IEnumerator BubbleHasCat()
    {
        yield return new WaitForSeconds(_timeBeforeBubbleHasCat);

        _bubbleBoomObject.SetActive(false);

        _catObject.transform.position = _bubbleObject.transform.position;
        _catObject.transform.parent = _bubbleObject.transform;
        _bubbleObject.SetActive(true);
        _catObject.SetActive(true);

        StartCoroutine(LoadFirstLevel());
    }

    IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(_timeBeforeLoadFirstLevel);

        //GameManager.Instance.LoadFirstLevel();
        LevelManager.Instance.LoadFirstLevel();
    }
}
