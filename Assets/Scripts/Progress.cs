using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string CurrentSceneName;
    public int Score;
    public int ChestCount;
    //public int LevelChestCount;
    public bool IsSoundOn = true;
    public bool IsZoom = false;
    public int DifficultyLvl = 1;
    public int JoystickPos = 2;
}

public class Progress: MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public static Progress Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            // DontDestroyOnLoad works only on root GameObjects
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
