using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject _player;
    private GameObject _spawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
        _player = Player.Instance.gameObject;
        _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        Respawn();
    }

    public void Respawn()
    {
        if (_player != null && _spawnPoint != null)
        {
            _player.transform.position = _spawnPoint.transform.position;
            _player.GetComponent<Player>().Reset();
        }
        else
        {
            Debug.Log("Player is null or spawnPoint is null!");
        }
    }
}
