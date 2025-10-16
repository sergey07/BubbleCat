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

    public void ShowChestCounter()
    {
        if (_chestCounterPanel != null)
        {
            _chestCounterPanel.SetActive(true);

            if (Progress.Instance == null)
            {
                Debug.Log("Progress.Instance is null!");
            }

            if (_txtChestCounter == null)
            {
                Debug.Log("_txtChestCounter is null!");
            }

            UpdateChestCounterText();
        }
    }

    public void hideChestCounter()
    {
        if (_chestCounterPanel != null)
        {
            _chestCounterPanel.SetActive(false);
        }
    }

    public void ResetChestCount()
    {
        Progress.Instance.PlayerInfo.ChestCount = 0;
        Progress.Instance.Save();
        UpdateChestCounterText();
    }

    public int GetScoreForChest()
    {
        return _scoreForChest;
    }

    public void CollectChest()
    {
        Progress.Instance.PlayerInfo.ChestCount += 1;
        UpdateChestCounterText();
    }

    private void UpdateChestCounterText()
    {
        _txtChestCounter.text = Progress.Instance.PlayerInfo.ChestCount.ToString();
    }
}
