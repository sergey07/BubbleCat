using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("Size Configuration")]
    // Original size of the bubble
    [SerializeField] private float _originSize = 3f;
    // Min size of the bubble
    [SerializeField] private float _minSize = 2f;
    // Max size of the bubble
    [SerializeField] private float _maxSize = 100f;

    [Header("Speed Configuration")]
    // The speed of changing size of the bubble
    [SerializeField] private float _scaleSpeed = 1f;

    [Header("Game Objects")]
    // Burst bubble splash object
    [SerializeField] private GameObject _bubbleBoomObject;

    [Header("Sound Configuration")]
    // The sound of a bubble bursting
    [SerializeField] public AudioClip _audioClipCpock;

    private Vector3 _originScale;
    //private bool _isFinish;

    // Start is called before the first frame update
    private void Start()
    {
        _originScale = new Vector3(_originSize, _originSize, _originSize);
        //_isFinish = false;
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

    // Gets different between current size of the bubble and its oroginal size
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

    // Resets size of the bubble to its original size
    public void ResetScale()
    {
        transform.localScale = _originScale;
    }

    // Bubble burst
    public void Boom()
    {
        if (_bubbleBoomObject != null)
        {
            _bubbleBoomObject.transform.parent = null;
        }

        _bubbleBoomObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isFinish = Player.Instance.IsFinish();

        if (!isFinish && collision.gameObject.CompareTag("FinishTrigger"))
        {
            Player.Instance.SetFinish(true);
            //GameManager.Instance.FinishLevel();
            LevelManager.Instance.FinishLevel();
        }
    }
}
