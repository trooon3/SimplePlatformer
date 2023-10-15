using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerCollisionHandler))]

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private bool _onGround;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private PlayerCollisionHandler _collisionHandler;
    private PlayerAnimator _animator;
    private float _radius = 0.3f;
    
    public int Damage => _damage;
    public bool OnGround => _onGround;

    private void Start()
    {
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        CheckStandOnGround();
    }

    private void CheckStandOnGround()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _radius, _ground);
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

    public void Heal(int healPoints)
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
}
