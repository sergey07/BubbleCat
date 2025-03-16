using UnityEngine;
using TMPro;

public class PausePanel : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private TextMeshProUGUI _txtSoundOff;
    [SerializeField] private TextMeshProUGUI _txtSoundOn;
    [SerializeField] private TextMeshProUGUI _txtZoomIn;
    [SerializeField] private TextMeshProUGUI _txtZoomOut;
    [SerializeField] private TextMeshProUGUI _txtLevelEasy;
    [SerializeField] private TextMeshProUGUI _txtLevelMedium;
    [SerializeField] private TextMeshProUGUI _txtLevelHard;
    [SerializeField] private TextMeshProUGUI _txtJoystickPosLeft;
    [SerializeField] private TextMeshProUGUI _txtJoystickPosRight;

    public void UpdateSoundButton(bool isSoundOn)
    {
        if (isSoundOn)
        {
            _txtSoundOff.gameObject.SetActive(true);
            _txtSoundOn.gameObject.SetActive(false);
        }
        else
        {
            _txtSoundOff.gameObject.SetActive(false);
            _txtSoundOn.gameObject.SetActive(true);
        }
    }

    public void UpdateZoomButton(bool isZoom)
    {
        if (isZoom)
        {
            _txtZoomIn.gameObject.SetActive(false);
            _txtZoomOut.gameObject.SetActive(true);
        }
        else
        {
            _txtZoomIn.gameObject.SetActive(true);
            _txtZoomOut.gameObject.SetActive(false);
        }
    }

    public void UpdateDifficultyButton(int difficultyLvl)
    {
        switch (difficultyLvl)
        {
            case 1:
                _txtLevelEasy.gameObject.SetActive(true);
                _txtLevelMedium.gameObject.SetActive(false);
                _txtLevelHard.gameObject.SetActive(false);
                break;
            case 2:
                _txtLevelEasy.gameObject.SetActive(false);
                _txtLevelMedium.gameObject.SetActive(true);
                _txtLevelHard.gameObject.SetActive(false);
                break;
            case 3:
                _txtLevelEasy.gameObject.SetActive(false);
                _txtLevelMedium.gameObject.SetActive(false);
                _txtLevelHard.gameObject.SetActive(true);
                break;
        }
    }

    public void UpdateJoystickPositionButton(int _joystickPosition)
    {
        switch (_joystickPosition)
        {
            case 1:
                _txtJoystickPosLeft.gameObject.SetActive(true);
                _txtJoystickPosRight.gameObject.SetActive(false);
                break;
            case 2:
                _txtJoystickPosLeft.gameObject.SetActive(false);
                _txtJoystickPosRight.gameObject.SetActive(true);
                break;
        }
    }
}
