using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    public void Respawn()
    {
        if (player != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.GetComponent<Player>().ResetScale();
        }
    }
}
