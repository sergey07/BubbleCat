using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float originSize = 1f;
    [SerializeField] private float minSize = 1f;
    [SerializeField] private float maxSize = 6f;
    [SerializeField] private float scaleSpeed = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        rb.MovePosition(rb.position + inputVector * (speed * Time.fixedDeltaTime));
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
}
