using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private GameObject bubbleObject;
    [SerializeField] private GameObject catObject;
    [SerializeField] private Transform endTriggerTransform;

    private Rigidbody2D rb;

    void Start()
    {
        rb = bubbleObject.gameObject.GetComponent<Rigidbody2D>();
        catObject.gameObject.GetComponent<Cat>().enabled = false;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(speed * Time.fixedDeltaTime, 0));

        if (bubbleObject.transform.position.x > endTriggerTransform.position.x)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    private void LateUpdate()
    {
        catObject.transform.position = bubbleObject.transform.position;
    }
}
