using UnityEngine;

public class TutorialToggler : MonoBehaviour
{
    [SerializeField] private GameObject _tutorPanel1;
    [SerializeField] private GameObject _tutorPanel2;
    [SerializeField] private float _delay = 0.1f;

    //private float _currentDelay = 0.0f;

    private void Awake()
    {
        GameManager.Instance.Pause();
    }

    private void Start()
    {
        _tutorPanel1.SetActive(true);
    }

    private void Update()
    {
        //_currentDelay += Time.deltaTime;

        if (Input.anyKey/* && _currentDelay >= _delay*/)
        {
            //_currentDelay = 0.0f;

            if (_tutorPanel1.activeInHierarchy)
            {
                _tutorPanel1.SetActive(false);
                _tutorPanel2.SetActive(true);
            }
            else if (_tutorPanel2.activeInHierarchy)
            {
                _tutorPanel2.SetActive(false);
                GameManager.Instance.Resume();
            }
        }
    }
}
