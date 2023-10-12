using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private bool _onGround;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private int Speed = Animator.StringToHash("Speed");
    private int Chop = Animator.StringToHash("Attack");
    
    private float _radius = 0.3f;
    private float _moveSpeed = 6f;
    private AudioSource _audioSource;

    public int Damage => _damage;

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
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            
            _animator.SetFloat(Speed, Mathf.Abs(_moveSpeed));
            transform.Translate(-_moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            _animator.SetFloat(Speed, Mathf.Abs(_moveSpeed));
            transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Attack();
        }

        CheckStandOnGround();
    }

    private void CheckStandOnGround()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _radius, _ground);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            TakeDamage(enemy.Damage);
        }
        
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            _audioSource.Play();
        }

        if (collision.TryGetComponent<MedicalKit>(out MedicalKit medicalKit))
        {
            Heal(medicalKit.HealPoints);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy) && Input.GetKey(KeyCode.S))
        {
            enemy.TakeDamage(_damage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Heal(int healPoints)
    {
        if (_health > 0)
        {
            _health += healPoints;
        }
        if (_health >= 10)
        {
            _health = 10;
        }
    }

    private void Attack()
    {
        _animator.Play(Chop);
    }
}
