using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    [SerializeField] float fallingSpeed = 5.0f;
    //[SerializeField] float offsetFromBottom = 0.0f;

    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject falledCatTrigger;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private Transform chpokSoundTriggerTransform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip audioClipCpock;
    [SerializeField] public AudioClip audioClipMau;
    [SerializeField] public AudioClip audioClipBulk;
    //[SerializeField] private GameObject bottomEdgeBubblePoint;

    private Rigidbody2D rb;

    private bool _isFalling = false;

    private bool isCpock = false;

    // Start is called before the first frame update
    void Start()
    {
        // Disable parent scaling
        //transform.parent = null;


        rb = GetComponent<Rigidbody2D>();

        if (SceneManager.GetActiveScene().name == "CatDied")
        {
            _isFalling = true;
        }
    }

    private void LateUpdate()
    {
        if (!_isFalling)
        {
            if (playerTransform != null)
            {
                transform.position = playerTransform.position;// + new Vector3(0, bottomEdgeBubblePoint.transform.position.y + offsetFromBottom);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isFalling)
        {
            rb.MovePosition(rb.position - new Vector2(0, fallingSpeed * Time.fixedDeltaTime));
            if (!isCpock && transform.position.y < chpokSoundTriggerTransform.position.y)
            {
                Debug.Log(chpokSoundTriggerTransform);
                isCpock = true;
                audioSource.PlayOneShot(audioClipBulk);
            }
        }
    }

    public void SetFalling(bool isFalling)
    {
        _isFalling = isFalling;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FalledCatTrigger"))
        {
            GameProgress.currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("CatDied");
        }
        else if (collision.gameObject.CompareTag("Boiler"))
        {
            SceneManager.LoadScene(GameProgress.currentSceneName);
        }
    }
}
