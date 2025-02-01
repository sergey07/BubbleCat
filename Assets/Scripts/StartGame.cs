using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // ����� � �������� �� ����������� ���� ��� ������
    [SerializeField] private float _timeBeforeTranslateCatByWitch = 2.0f;
    // ����� � �������� �� ������������� ���� � ����
    [SerializeField] private float _timeBeforeBoilerBoils = 2.0f;
    // ����� � �������� ����� ������������� ������ � ����� ������
    [SerializeField] private float _timeBeforeBubbleHasCat = 2.0f;
    // ����� � �������� ����� ��������� ������� ������ ����
    [SerializeField] private float _timeBeforeLoadFirstLevel = 1.0f;
    [SerializeField] private float _catLocalScale = 1.0f;

    [SerializeField] private Animator _animator;

    // ������ ������
    [SerializeField] private GameObject _witchObject;
    // ������ ������
    [SerializeField] private GameObject _playerObject;
    // ������ ����
    [SerializeField] private GameObject _catObject;
    // ������ ������
    [SerializeField] private GameObject _bubbleObject;
    // ������ ����� �� ���������� ������
    [SerializeField] private GameObject _bubbleBoomObject;
    // �����, ��� ���������� ���
    [SerializeField] private Transform _spawnPoint;

    // ������ ������ � ����� � �����
    [SerializeField] private Sprite _witchWithCat;
    // ������ ������ ��� ����
    [SerializeField] private Sprite _witchWithoutCat;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.SetPlayerStatus(PlayerStatus.InStartGameScene);

        _playerObject.SetActive(false);
        _catObject.transform.localScale = new Vector3(_catLocalScale, _catLocalScale, _catLocalScale);
        _playerObject.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        //_bubbleObject.SetActive(false);
        //_bubbleBoomObject.SetActive(false);

        _playerObject.transform.position = _spawnPoint.position;
        //_bubbleObject.transform.position = _bubbleBoomObject.transform.position;
        //_catObject.transform.position = _bubbleBoomObject.transform.position;

        //StartCoroutine(WaitForLoadFirstLevel());
        StartCoroutine(TranslateCatByWitch());
    }

    // ������ ���������� ���� ��� ������
    IEnumerator TranslateCatByWitch()
    {
        yield return new WaitForSeconds(_timeBeforeTranslateCatByWitch);

        // ������������ ������ �� ��� �
        _witchObject.transform.localScale = new Vector3(-_witchObject.transform.localScale.x, _witchObject.transform.localScale.y, _witchObject.transform.localScale.z);
        //catObject.transform.parent = null;

        _animator.SetTrigger("boom");

        StartCoroutine(BoilerBoils());
    }

    // ���������� ����� �� �����
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
