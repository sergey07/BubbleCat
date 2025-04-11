using TMPro;
using UnityEngine;

public class FinishGamePanel : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private TextMeshProUGUI _txtGameTotal;

    private void Start()
    {
        _txtGameTotal.text = Progress.Instance.PlayerInfo.Score.ToString();
    }

    public void NewGame()
    {
        GameManager.Instance.LoadFirstLevel();
    }

    //private int CalculateScoreForChests(int scoreForChest, int chestCount)
    //{
    //    return scoreForChest * chestCount;
    //}

    //private int CalculateTotal(int scoreForChests, int remainingSeconds)
    //{
    //    return scoreForChests + remainingSeconds;
    //}

    //private void SaveScore(int levelTotalScore)
    //{
    //    Progress.Instance.PlayerInfo.Score += levelTotalScore;
    //}
}
