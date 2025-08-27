using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("Size Configuration")]
    // Original size of the bubble
    [SerializeField] private float _originSize = 3f;
    // Min size of the bubble
    [SerializeField] private float _minSize = 2f;
    // Max size of the bubble
    [SerializeField] private float _maxSize = 100f;

    [Header("Speed Configuration")]
    // The speed of changing size of the bubble
    [SerializeField] private float _scaleSpeed = 1f;

    [Header("Game Objects")]
    // Burst bubble splash object
    [SerializeField] private GameObject _bubbleBurstObject;

    [Header("Sound Configuration")]
    // The sound of a bubble bursting
    [SerializeField] private AudioClip _audioClipBurst;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;

    private Vector3 _originScale;

    public void InitBubble()
    {
        _originScale = new Vector3(_originSize, _originSize, _originSize);
        ResetScale();
    }

    // Gets different between current size of the bubble and its oroginal size
    public float GetDeltaScale()
    {
        float deltaScale = transform.localScale.x - _originSize;
        return deltaScale;
    }

    public void IncreaseScale()
    {
        float scaleValue = ChangeScale(_scaleSpeed);
        transform.localScale = new Vector3(scaleValue, scaleValue, transform.localScale.y);
    }

    public void DecreaseScale()
    {
        float scaleValue = ChangeScale(-_scaleSpeed);
        transform.localScale = new Vector3(scaleValue, scaleValue, transform.localScale.y);
    }

    public void ChangeScaleToOrigin()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(_originSize, _originSize, _originSize), _scaleSpeed * Time.fixedDeltaTime);
    }

    // Resets size of the bubble to its original size
    public void ResetScale()
    {
        transform.localScale = _originScale;
    }

    // Bubble burst
    public void Burst()
    {
        if (_bubbleBurstObject == null)
        {
            return;
        }

        _bubbleBurstObject.transform.parent = null;
        _bubbleBurstObject.SetActive(true);
    }

    public void PlaySoundBurst()
    {
        _audioSource.PlayOneShot(_audioClipBurst);
    }

    private float ChangeScale(float scaleSpeed)
    {
        float newScaleX = Mathf.Clamp(transform.localScale.x + scaleSpeed * Time.fixedDeltaTime, _minSize, _maxSize);
        return newScaleX;
    }
}
