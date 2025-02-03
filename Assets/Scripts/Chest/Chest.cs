using UnityEngine;

[SelectionBase]
public class Chest : MonoBehaviour
{
    [SerializeField] private int _reward = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.AddReward(_reward);
        Destroy(gameObject);
    }
}
