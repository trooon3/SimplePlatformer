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
    [SerializeField] private float _damage;

    private PlayerCollisionHandler _collisionHandler;
    private PlayerAnimator _animator;
    private float _radius = 0.3f;
    private float _maxHealth = 20f;
    private float _minHealth = 0;

    public float MaxHealth => _maxHealth;
    public float CurrentHealthPoints { get; private set; }
    public float Damage => _damage;
    public bool OnGround => _onGround;

    public UnityAction HealthChanged;

    private void Start()
    {
        CurrentHealthPoints = _maxHealth;
        HealthChanged?.Invoke();
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

    public void TakeDamage(float damage)
    {
        CurrentHealthPoints = Mathf.Clamp(CurrentHealthPoints -= damage, _minHealth, _maxHealth);
        HealthChanged?.Invoke();

        if (CurrentHealthPoints <= _minHealth)
        {
            HealthChanged?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void Heal(float healPoints)
    {
        CurrentHealthPoints = Mathf.Clamp(CurrentHealthPoints += healPoints, _minHealth, _maxHealth);
        HealthChanged?.Invoke();
    }
}
