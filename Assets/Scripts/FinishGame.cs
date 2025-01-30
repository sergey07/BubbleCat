using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private GameObject _playerObject;
    //[SerializeField] private GameObject _bubbleObject;
    //[SerializeField] private GameObject _catObject;
    [SerializeField] private Transform _endTriggerTransform;

    private Rigidbody2D _rb;

    void Start()
    {
        Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
        _rb = _playerObject.gameObject.GetComponent<Rigidbody2D>();
        //_rb = _bubbleObject.gameObject.GetComponent<Rigidbody2D>();
        //_catObject.gameObject.GetComponent<Cat>().enabled = false;
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + new Vector2(_speed * Time.fixedDeltaTime, 0));

        //if (_bubbleObject.transform.position.x > _endTriggerTransform.position.x)
        if (_playerObject.transform.position.x > _endTriggerTransform.position.x)
        {
            //SceneManager.LoadScene("Level1");
            GameManager.Instance.LoadFirstLevel();
        }
    }

    private void LateUpdate()
    {
        //_catObject.transform.position = _bubbleObject.transform.position;
    }
}
