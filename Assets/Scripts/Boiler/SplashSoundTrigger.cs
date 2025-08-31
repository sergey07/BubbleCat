using UnityEngine;

public class SplashSoundTrigger : MonoBehaviour
{
    private bool _isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isTriggered = true;
        }
    }

    // Returns information about whether the cat touched the trigger or not
    public bool IsTriggered()
    {
        return _isTriggered;
    }
}
