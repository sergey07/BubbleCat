using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5.0f;
    [SerializeField] private float maxYSpeed = 20.0f;
    [SerializeField] private float originSize = 1f;
    [SerializeField] private float minSize = 1f;
    [SerializeField] private float maxSize = 100f;
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private float ySpeedMultiplayer = 2f;

    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject catObject;

    private Rigidbody2D rb;

    private Vector3 originScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        originScale = new Vector3(originSize, originSize, originSize);
        ResetScale();
    }

    private void FixedUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        if (inputVector.y > 0)
        {
            float scaleValue = ChangeScale(scaleSpeed);
            transform.localScale = new Vector3(scaleValue, scaleValue, transform.localScale.y);
        }
        else if (inputVector.y < 0)
        {
            float scaleValue = ChangeScale(-scaleSpeed);
            transform.localScale = new Vector3(scaleValue, scaleValue, transform.localScale.y);
        }
        else
        {
            ChangeScaleToOrigin();
        }

        float velocityY = (transform.localScale.x - originSize) * ySpeedMultiplayer;
        velocityY = Mathf.Clamp(velocityY, -maxYSpeed, maxYSpeed);

        float newPosX = inputVector.x * (horizontalSpeed * Time.fixedDeltaTime);
        float newPosY = velocityY * Time.fixedDeltaTime; 

        rb.MovePosition(rb.position + new Vector2(newPosX, newPosY));
    }

    private float ChangeScale(float scaleSpeed)
    {
        float newScaleX = Mathf.Clamp(transform.localScale.x + scaleSpeed * Time.fixedDeltaTime, minSize, maxSize);
        

        return newScaleX;
    }

    private void ChangeScaleToOrigin()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(originSize, originSize, originSize), scaleSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            catObject.GetComponent<Cat>().SetFalling(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishTrigger"))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName != "Level2")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        ResetScale();
    }

    private void ResetScale()
    {
        transform.localScale = originScale;
    }
}
