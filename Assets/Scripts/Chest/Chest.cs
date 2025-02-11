using System.Collections;
using UnityEngine;

[SelectionBase]
public class Chest : MonoBehaviour
{
    [SerializeField] private int _reward = 1;

    [Header("Sound Configuration")]
    [SerializeField] public AudioClip _audioClipCollectChest;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audioSource.PlayOneShot(_audioClipCollectChest);
        GameManager.Instance.AddReward(_reward);
        //_audioClipCollectChest.length
        //Destroy(gameObject);
        StartCoroutine(DestroyChest(_audioClipCollectChest.length));
    }

    IEnumerator DestroyChest(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
