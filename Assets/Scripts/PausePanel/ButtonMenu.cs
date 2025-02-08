using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; 
    public void MenuToggler()
    {
        if (pausePanel.activeSelf)
        {
            MenuOff();
        }
        else
        {
            MenuOn();
        }
    }
    private void MenuOff()
    {
        pausePanel.SetActive(false);
    }
    private void MenuOn()
    {
        pausePanel.SetActive(true);
    }
}
