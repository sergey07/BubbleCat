using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [Header("Speed Configuration")]
    [SerializeField] private float _speed = 10.0f;
    [Header("Game Objects")]
    [SerializeField] private GameObject _playerObject;
    [Space]
    [SerializeField] private Transform _endTriggerTransform;

    private Rigidbody2D _rb;

    void Start()
    {
        Player.Instance.SetPlayerStatus(PlayerStatus.InFinishGameScene);
        _rb = _playerObject.gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + new Vector2(_speed * Time.fixedDeltaTime, 0));

        if (_playerObject.transform.position.x > _endTriggerTransform.position.x)
        {;
            GameManager.Instance.LoadFirstLevel();
        }
    }
}
