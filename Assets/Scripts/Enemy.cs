using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    
    public Player Target => _target;
    public int Damage => _damage;

    public void ApplyDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        if (_health < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            TakeDamage(player.Damage);
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
}
