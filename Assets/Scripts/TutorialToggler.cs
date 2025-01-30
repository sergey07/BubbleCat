using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggler : MonoBehaviour
{
    [SerializeField] private GameObject _tutorPanel;

    // start
    void Start()
    {
        tutorPanelEnable();
    }

    // Update
    void Update()
    {
        UpdateMaker();
    }

    private void UpdateMaker()
    {
        // DisplayTime(Time.time);
        CheckAnyButton();
    }

    private void tutorPanelEnable()
    {
        _tutorPanel.SetActive(true);
    }

    private void CheckAnyButton()
    {
        if (Input.anyKey)
        {
            tutorPanelDisable();
        }
    }

    private void tutorPanelDisable()
    {
        _tutorPanel.SetActive(false);
    }
}
