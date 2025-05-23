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
}
