using System.Collections;
using UnityEngine;

[SelectionBase]
public class Chest : MonoBehaviour
{
    [SerializeField] private int _reward = 1;

    [Header("Sound Configuration")]
    [SerializeField] private AudioClip _audioClipCollectChest;

    [Header("Game Objects")]
    [SerializeField] private GameObject _chestVisual;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            _audioSource.PlayOneShot(_audioClipCollectChest);
            ChestManager.Instance.AddReward(_reward);
            _chestVisual.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyChest(_audioClipCollectChest.length));
        }
    }

    IEnumerator DestroyChest(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
