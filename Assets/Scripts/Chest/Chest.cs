using System.Collections;
using UnityEngine;

[SelectionBase]
public class Chest : MonoBehaviour
{
    [Header("Sound Configuration")]
    [SerializeField] private AudioClip _audioClipCollectChest;

    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;

    private bool _isHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble") && !_isHit)
        {
            _isHit = true;
            _audioSource.PlayOneShot(_audioClipCollectChest);
            ChestManager.Instance.CollectChest();
            _spriteRenderer.enabled = false;
            StartCoroutine(DestroyChest(_audioClipCollectChest.length));
        }
    }

    IEnumerator DestroyChest(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
