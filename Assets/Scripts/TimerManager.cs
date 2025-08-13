using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] private GameObject _timerPanel;
    [SerializeField] private TextMeshProUGUI _txtRemainingSeconds;
    [SerializeField] private TextMeshProUGUI _txtPrefix;

    [Header("Parametrs")]
    [SerializeField] private float _timeForLevel = 120.0f;

    private float _remainingTime;
    private int _remainingSeconds;
    private bool _timerIsRunning;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _timerIsRunning = true;

        if (_timerPanel != null)
        {
            if (Player.Instance.GetPlayerStatus() == PlayerStatus.InGame)
            {
                _timerPanel.SetActive(true);
                ResumeTimer();
            }
            else
            {
                _timerPanel.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (_timerIsRunning)
        {
            if (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
                UpdateTimerView();
            }
            else
            {
                _remainingTime = 0;
                _timerIsRunning = false;
                TimerEnded();
            }
        }
    }

    public void StopTimer()
    {
        _timerIsRunning = false;
    }

    public void ResumeTimer()
    {
        _timerIsRunning = true;
    }

    public void ResetTimer()
    {
        _remainingTime = _timeForLevel;
        _txtRemainingSeconds.text = _remainingTime.ToString();
    }

    public int GetRemainingSeconds()
    {
        return _remainingSeconds;
    }

    private void UpdateTimerView()
    {
        // Added 1 because FloorToInt returns the largest integer smaller to or equal to argument
        _remainingSeconds = Mathf.FloorToInt(_remainingTime + 1);
        _txtRemainingSeconds.text = _remainingSeconds.ToString();
    }

    private void TimerEnded()
    {
        Player.Instance.Fall();
    }
}
