using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkSoundTrigger : MonoBehaviour
{
    private bool _isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            _isTriggered = true;
        }
    }

    // ���������� ���������� � ���, �������� �� ����� �������� ��� ���
    public bool IsTriggered()
    {
        return _isTriggered;
    }
}
