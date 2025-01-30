using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatSprite { Normal, Flying }

public class CatVisual : MonoBehaviour
{
    // Спрайт кота в обычном сотстоянии
    [SerializeField] private Sprite _normalCat;
    // Спрайт падающего кота
    [SerializeField] private Sprite _flyingCat;

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

    // Устанавливает изображение кота
    public void SetCatSprite(CatSprite catSprite)
    {
        switch (catSprite)
        {
            case CatSprite.Flying:
                GetComponent<SpriteRenderer>().sprite = _flyingCat;
                break;
            case CatSprite.Normal:
            default:
                GetComponent<SpriteRenderer>().sprite = _normalCat;
                break;
        }
    }
}
