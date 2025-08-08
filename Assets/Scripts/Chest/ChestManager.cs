using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestManager : MonoBehaviour
{
    public static ChestManager Instance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] private GameObject _chestCounterPanel;
    [SerializeField] private TextMeshProUGUI _txtChestCounter;

    [Header("Parametrs")]
    [SerializeField] private int _scoreForChest = 10;

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
        if (_chestCounterPanel != null)
        {
            if (SceneManager.GetActiveScene().name == "StartScene")
            {
                _chestCounterPanel.SetActive(false);
            }
            else
            {
                _chestCounterPanel.SetActive(true);
                //int allChestCount = Progress.Instance.PlayerInfo.LevelChestCount + Progress.Instance.PlayerInfo.ChestCount;

                if (Progress.Instance == null)
                {
                    Debug.Log("Progress.Instance is null!");
                }

                if (_txtChestCounter == null)
                {
                    Debug.Log("_txtChestCounter is null!");
                }

                _txtChestCounter.text = Progress.Instance.PlayerInfo.ChestCount.ToString();
            }
        }
    }

    public int GetScoreForChest()
    {
        return _scoreForChest;
    }

    public void AddReward(int reward)
    {
        Progress.Instance.PlayerInfo.ChestCount += reward;
        //int allChestCount = Progress.Instance.PlayerInfo.LevelChestCount + Progress.Instance.PlayerInfo.ChestCount;
        _txtChestCounter.text = Progress.Instance.PlayerInfo.ChestCount.ToString();
    }
}
