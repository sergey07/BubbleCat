using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private GameObject _player;
    private GameObject _spawnPoint;

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
    public void Spawn()
    {
        if (_player == null)
        {
            _player = Player.Instance.gameObject;
        }

        if (_spawnPoint == null)
        {
            _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        }

        _player.transform.position = _spawnPoint.transform.position;
        _player.GetComponent<Player>().Reset();
    }
}
