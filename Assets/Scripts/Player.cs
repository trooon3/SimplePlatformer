using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _onGround;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;

    private float _radius = 0.3f;
    private float _moveSpeed = 6f;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && _onGround)
        {
            _rigidbody2D.velocity = new Vector2(0f, _moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _animator.SetFloat("speed", Mathf.Abs(_moveSpeed));
            transform.Translate(-_moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetFloat("speed", Mathf.Abs(_moveSpeed));
            transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
        }

        CheckStandOnGround();
    }

    private void CheckStandOnGround()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _radius, _ground);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyAttackZone>(out EnemyAttackZone enemyAttackZone))
        {
          Destroy(_player);
        }
        
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            _audioSource.Play();
        }
    }
}
