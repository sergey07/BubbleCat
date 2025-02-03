using UnityEngine;

[SelectionBase]
public class Chest : MonoBehaviour
{
    [SerializeField] private int _reward = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameProgress.collectedChestCount += _reward;
        Destroy(gameObject);
    }
}
