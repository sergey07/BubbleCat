using TMPro;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private TextMeshProUGUI _txtChestCount;
    [SerializeField] private TextMeshProUGUI _txtScoreForChest;
    [SerializeField] private TextMeshProUGUI _txtRemainingSeconds;
    [SerializeField] private TextMeshProUGUI _txtTotal;

    public void UpdatePanel(int chestCount, int scoreForChest, int remainingSeconds)
    {
        int scoreForChests = CalculateScoreForChests(scoreForChest, chestCount);
        int total = CalculateTotal(scoreForChests, remainingSeconds);

        SaveScore(total);

        _txtChestCount.text = chestCount.ToString();
        _txtScoreForChest.text = scoreForChest.ToString();
        _txtRemainingSeconds.text = remainingSeconds.ToString();
        _txtTotal.text = total.ToString();
    }

    private int CalculateScoreForChests(int scoreForChest, int chestCount)
    {
        return scoreForChest * chestCount;
    }

    private int CalculateTotal(int scoreForChests, int remainingSeconds)
    {
        return scoreForChests + remainingSeconds;
    }

    private void SaveScore(int levelTotalScore)
    {
        Progress.Instance.PlayerInfo.Score += levelTotalScore;
    }
}
