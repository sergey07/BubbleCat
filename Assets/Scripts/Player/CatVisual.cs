using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatSprite { Normal, Flying }

public class CatVisual : MonoBehaviour
{
    // The cat's sprite in normal state
    [SerializeField] private Sprite _normalCat;
    // The falling cat's sprite
    [SerializeField] private Sprite _fallingCat;

    // Update is called once per frame
    void Update()
    {
        PlayerStatus playerStatus = Player.Instance.GetPlayerStatus();

        if (playerStatus == PlayerStatus.InGame)
        {
            HandleInput();
        }
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

    public void SetCatSprite(CatSprite catSprite)
    {
        switch (catSprite)
        {
            case CatSprite.Flying:
                GetComponent<SpriteRenderer>().sprite = _fallingCat;
                break;
            case CatSprite.Normal:
            default:
                GetComponent<SpriteRenderer>().sprite = _normalCat;
                break;
        }
    }
}
