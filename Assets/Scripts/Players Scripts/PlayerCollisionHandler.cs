using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(AudioSource))]

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    private PlayerAnimator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            _audioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyFightController enemy) && Input.GetKey(KeyCode.S))
        {
            enemy.TakeDamage(_player.Damage);
            _animator.AttackAnimation();
        }
    }
}
