using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private TextMeshProUGUI _txtSoundOff;
    [SerializeField] private TextMeshProUGUI _txtSoundOn;
    [SerializeField] private TextMeshProUGUI _txtJoystickPositionLabel;
    [SerializeField] private Button _btnJoystickPos;
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

    public void UpdateJoystickPositionButton(int _joystickPosition, bool isJoystickVisible)
    {
        if (isJoystickVisible)
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
        else
        {
            _txtJoystickPositionLabel.gameObject.SetActive(false);
            _btnJoystickPos.gameObject.SetActive(false);
        }
    }
}
