using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatVisual : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        if (inputVector.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (inputVector.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
