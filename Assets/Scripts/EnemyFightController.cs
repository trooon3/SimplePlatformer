using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFightController : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    private float _minHealth = 0;
    
    public Player Target => _target;
    public float Damage => _damage;
    public float MaxHealth => _maxHealth;
    public float CurrentHealthPoints { get; private set; }

    public UnityAction HealthChanged;
    private void Start()
    {
        CurrentHealthPoints = _maxHealth;
        HealthChanged?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _target.TakeDamage(_damage);
            HealthChanged?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealthPoints = Mathf.Clamp(CurrentHealthPoints -= damage, _minHealth, _maxHealth);
        HealthChanged?.Invoke();
        Debug.Log("враg получил урон");
        if (CurrentHealthPoints <= _minHealth)
        {
            gameObject.SetActive(false);
        }
    }
}
