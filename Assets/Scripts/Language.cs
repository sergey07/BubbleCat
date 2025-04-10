using System.Runtime.InteropServices;
using UnityEngine;

public class Language : MonoBehaviour
{
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern string GetLang();
#endif

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
#if !UNITY_EDITOR && UNITY_WEBGL
            CurrentLanguage = GetLang();
#else
            CurrentLanguage = "ru";
#endif
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
