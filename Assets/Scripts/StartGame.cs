using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Время в секундах до перемещения кота над котлом
    [SerializeField] private float _timeBeforeTranslateCatByWitch = 2.0f;
    // Время в секундах до опрокидывания кота в котёл
    [SerializeField] private float _timeBeforeBoilerBoils = 2.0f;
    // Время в секундах перед формированием пузыря с котом внутри
    [SerializeField] private float _timeBeforeBubbleHasCat = 2.0f;
    // Время в секундах перед загрузкой первого уровня игры
    [SerializeField] private float _timeBeforeLoadFirstLevel = 1.0f;

    [SerializeField] private Animator _animator;

    // Объект ведьмы
    [SerializeField] private GameObject _witchObject;
    // Объект игрока
    [SerializeField] private GameObject _playerObject;
    // Объект кота
    [SerializeField] private GameObject _catObject;
    // Объект пузыря
    [SerializeField] private GameObject _bubbleObject;
    // Объект брызг от лопнувшего пузыря
    [SerializeField] private GameObject _bubbleBoomObject;
    // Место, где поялвяется кот
    [SerializeField] private Transform _spawnPoint;

    // Спрайт ведьмы с котом в руках
    [SerializeField] private Sprite _witchWithCat;
    // Спрайт ведьмы без кота
    [SerializeField] private Sprite _witchWithoutCat;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.SetPlayerStatus(PlayerStatus.InStartGameScene);

        _playerObject.SetActive(false);
        _catObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        _playerObject.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        //_bubbleObject.SetActive(false);
        //_bubbleBoomObject.SetActive(false);

        _playerObject.transform.position = _spawnPoint.position;
        //_bubbleObject.transform.position = _bubbleBoomObject.transform.position;
        //_catObject.transform.position = _bubbleBoomObject.transform.position;

        //StartCoroutine(WaitForLoadFirstLevel());
        StartCoroutine(TranslateCatByWitch());
    }

    // Ведьма перемещает кота над котлом
    IEnumerator TranslateCatByWitch()
    {
        yield return new WaitForSeconds(_timeBeforeTranslateCatByWitch);

        // Поворачиваем ведьму по оси Х
        _witchObject.transform.localScale = new Vector3(-_witchObject.transform.localScale.x, _witchObject.transform.localScale.y, _witchObject.transform.localScale.z);
        //catObject.transform.parent = null;

        _animator.SetTrigger("boom");

        StartCoroutine(BoilerBoils());
    }

    // Происходит взрыв из котла
    IEnumerator BoilerBoils()
    {
        yield return new WaitForSeconds(_timeBeforeBoilerBoils);

        _witchObject.GetComponent<SpriteRenderer>().sprite = _witchWithoutCat;
        _playerObject.SetActive(true);
        _bubbleObject.SetActive(false);
        _catObject.SetActive(true);
        _bubbleBoomObject.SetActive(true);

        //_bubbleBoomObject.transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(_newBubbleBoomScaleX, _newBubbleBoomScaleY, 1), _scaleBubbleBoomSpeed * Time.fixedDeltaTime);

        StartCoroutine(BubbleHasCat());
    }

    IEnumerator BubbleHasCat()
    {
        yield return new WaitForSeconds(_timeBeforeBubbleHasCat);

        _bubbleBoomObject.SetActive(false);

        //_playerObject
        _catObject.transform.position = _bubbleObject.transform.position;
        _catObject.transform.parent = _bubbleObject.transform;
        _bubbleObject.SetActive(true);

        StartCoroutine(LoadFirstLevel());
    }

    IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(_timeBeforeLoadFirstLevel);

        GameManager.Instance.LoadFirstLevel();
    }
}
