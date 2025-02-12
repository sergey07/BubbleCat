using UnityEngine;

public class Language : MonoBehaviour
{
    public static Language Instance;

    public string CurrentLanguage {  get; set; } // ru en

    private void Awake()
    {
        if (Instance == null)
        {
            // DontDestroyOnLoad works only on root GameObjects
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;

            // TODO: change "ru" to value from Yandex SDK
            CurrentLanguage = "ru";
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
