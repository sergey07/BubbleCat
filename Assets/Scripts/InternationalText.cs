using TMPro;
using UnityEngine;

public class InternationalText : MonoBehaviour
{
    [Multiline]
    [SerializeField] private string _en;
    [Multiline]
    [SerializeField] private string _ru;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (Language.Instance.CurrentLanguage == "en")
        {
            _text.text = _en;
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            _text.text = _ru;
        }
        else
        {
            _text.text = _en;
        }
    }
}
