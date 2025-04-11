using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("GameObjects")]
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private TextMeshProUGUI _txtPrefix;

    private int _score = 0;

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
        if (_scorePanel != null)
        {
            if (SceneManager.GetActiveScene().name == "StartScene")
            {
                _scorePanel.SetActive(false);
            }
            else
            {
                _scorePanel.SetActive(true);
                _score = Progress.Instance.PlayerInfo.Score;
                UpdateView(_score);
            }
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        Progress.Instance.PlayerInfo.Score = _score;
        UpdateView(_score);
    }

    public void UpdateView(int score)
    {
        string strZeroList;

        if (score < 10)
        {
            strZeroList = "000000";
        }
        else if (score < 100)
        {
            strZeroList = "00000";
        }
        else if (score < 1000)
        {
            strZeroList = "0000";
        }
        else if (score < 10000)
        {
            strZeroList = "000";
        }
        else if (score < 100000)
        {
            strZeroList = "00";
        }
        else if (score < 1000000)
        {
            strZeroList = "0";
        }
        else
        {
            strZeroList = "";
        }

        _txtScore.text = strZeroList + score.ToString();
    }
}
