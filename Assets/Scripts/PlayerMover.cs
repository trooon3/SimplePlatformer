using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private float _moveSpeed = 6f;

    public float MoveSpeed => _moveSpeed;

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<PlayerAnimator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && _player.OnGround)
        {
            _rigidbody2D.velocity = new Vector2(0f, _moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            _animator.WalkAnimation();
            transform.Translate(-_moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            _animator.WalkAnimation();
            transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _animator.AttackAnimation();
        }
    }
}
