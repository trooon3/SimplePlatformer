using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightController : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    
    public Player Target => _target;
    public int Damage => _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _target.TakeDamage(_damage);
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
