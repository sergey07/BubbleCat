using UnityEngine;
using System.Runtime.InteropServices;

public class YandexAdv : MonoBehaviour
{
    [SerializeField] private GameObject _resurrectPanel;

    public void ShowResurrectPanel()
    {
        GameManager.Instance.Pause();
        _resurrectPanel.gameObject.SetActive(true);
    }

    public void HideResurrectPanel()
    {
        _resurrectPanel.gameObject.SetActive(false);
    }

    public void ShowAdv()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        Resurrect();
#else
        Debug.Log("Resurrect");
        //GameManager.Instance.Resurrect();
        HideResurrectPanel();
        LevelManager.Instance.RestartLevel();
#endif
    }

    private void Resurrect()
    {
        Yandex.Instance.Resurrect();
    }
}
