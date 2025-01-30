using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
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
