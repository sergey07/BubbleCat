using UnityEngine;

public enum CatSprite { Normal, Flying }

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    [Header("Sprites")]
    // The cat's sprite in normal state
    [SerializeField] private Sprite _normalCat;
    // The falling cat's sprite
    [SerializeField] private Sprite _fallingCat;

    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetFalling(bool isFalling)
    {
        if (isFalling)
        {
            SetCatSprite(CatSprite.Flying);
        }
        else
        {
            SetCatSprite(CatSprite.Normal);
        }
    }

    public void FlipRight()
    {
        _spriteRenderer.flipX = true;
    }

    public void FlipLeft()
    {
        _spriteRenderer.flipX = false;
    }

    private void SetCatSprite(CatSprite catSprite)
    {
        switch (catSprite)
        {
            case CatSprite.Flying:
                _spriteRenderer.sprite = _fallingCat;
                break;
            case CatSprite.Normal:
            default:
                _spriteRenderer.sprite = _normalCat;
                break;
        }
    }
}
