using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // ���� ������� ������
    [SerializeField] public AudioClip _audioClipCpock;
    // ������ ����� ���������� ������
    [SerializeField] private GameObject _bubbleBoomObject;

    // ������������ ������ ������
    [SerializeField] private float _originSize = 3f;
    // ����������� ������ ������
    [SerializeField] private float _minSize = 2f;
    // ������������ ������ ������
    [SerializeField] private float _maxSize = 100f;
    // �������� ��������� ������� ������
    [SerializeField] private float _scaleSpeed = 1f;

    private Vector3 _originScale;

    // Start is called before the first frame update
    private void Start()
    {
        _originScale = new Vector3(_originSize, _originSize, _originSize);
        ResetScale();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Player.Instance.GetPlayerStatus() == PlayerStatus.InGame)
        {
            HandleInput();
        }
    }

    // ���������� ������� ����� ������� �������� ������ � ��� ������������ ��������
    public float GetDeltaScale()
    {
        float deltaScale = transform.localScale.x - _originSize;
        return deltaScale;
    }

    private void HandleInput()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        if (inputVector.y > 0)
        {
            float scaleValue = ChangeScale(_scaleSpeed);
            transform.localScale = new Vector3(scaleValue, scaleValue, transform.localScale.y);
        }
        else if (inputVector.y < 0)
        {
            float scaleValue = ChangeScale(-_scaleSpeed);
            transform.localScale = new Vector3(scaleValue, scaleValue, transform.localScale.y);
        }
        else
        {
            ChangeScaleToOrigin();
        }
    }

    private float ChangeScale(float scaleSpeed)
    {
        float newScaleX = Mathf.Clamp(transform.localScale.x + scaleSpeed * Time.fixedDeltaTime, _minSize, _maxSize);
        return newScaleX;
    }

    private void ChangeScaleToOrigin()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(_originSize, _originSize, _originSize), _scaleSpeed * Time.fixedDeltaTime);
    }

    // ���������� ������ ������ �� ��� ������������� �������
    public void ResetScale()
    {
        transform.localScale = _originScale;
    }

    // ������� ������
    public void Boom()
    {
        if (_bubbleBoomObject != null)
        {
            _bubbleBoomObject.transform.parent = null;
        }

        _bubbleBoomObject.SetActive(true);

        StartCoroutine(DestroyBubbleBoom());
    }

    IEnumerator DestroyBubbleBoom()
    {
        yield return new WaitForSeconds(0.1f);
        _bubbleBoomObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishTrigger"))
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
}
