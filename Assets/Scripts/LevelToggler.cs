using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToggler : MonoBehaviour
{
    public static LevelToggler Instance { get; private set; }

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _levels;

    private int _curLevel = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    public void FirstLevel()
    {
        _curLevel = 0;
        UpdateLevel(_curLevel);
    }

    public void NextLevel()
    {
        if (IsLastLevel())
        {
            Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
            SceneManager.LoadScene("FinishGame");
        }
        else
        {
            _curLevel++;
            UpdateLevel(_curLevel);
        }
    }

    public bool IsLastLevel()
    {
        return _curLevel == _levels.Length - 1;
    }

    public void RestartLevel()
    {
        UpdateLevel(_curLevel);
    }

    private void UpdateLevel(int curLevel)
    {
        // Reset phisic
        _player.GetComponent<Rigidbody>().isKinematic = true;
        _player.GetComponent<Rigidbody>().isKinematic = false;

        //_player.transform.position = new Vector3(0, 60, -1.5f);

        for (int i = 0; i < _levels.Length; i++)
        {
            if (i == curLevel)
            {
                _levels[i].SetActive(true);
            }
            else
            {
                _levels[i].SetActive(false);
            }
        }
    }
}
