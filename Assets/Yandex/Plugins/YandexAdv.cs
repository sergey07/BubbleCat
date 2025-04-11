using UnityEngine;
using System.Runtime.InteropServices;

public class YandexAdv : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void ResurrectExtern();
#endif

    [SerializeField] private GameObject _resurrectPanel;

    public void ShowResurrectPanel()
    {
        GameManager.Instance.Pause();
        _resurrectPanel.SetActive(true);
    }

    public void ShowAdvButton()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        ResurrectExtern();
#endif
    }
}
