using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestManager : MonoBehaviour
{
    public static ChestManager Instance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] private GameObject _chestCounterPanel;
    [SerializeField] private TextMeshProUGUI _txtChestCounter;

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
                int allChestCount = Progress.Instance.PlayerInfo.LevelChestCount + Progress.Instance.PlayerInfo.ChestCount;
                _txtChestCounter.text = allChestCount.ToString();
            }
        }
    }

    public void AddReward(int reward)
    {
        Progress.Instance.PlayerInfo.LevelChestCount += reward;
        int allChestCount = Progress.Instance.PlayerInfo.LevelChestCount + Progress.Instance.PlayerInfo.ChestCount;
        _txtChestCounter.text = allChestCount.ToString();
    }
}
