using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiresPower : MonoBehaviour
{
    [SerializeField] private Player _player;
    private float _vampireHeal = 1;
    private float _vampireDamage = 1;
    private EnemyFightController _enemy;
    private WaitForSeconds _suckDelay = new WaitForSeconds(1f);
    private int _vampirismDuration = 6;
    private Coroutine _coroutine;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyFightController enemy))
        {
            _enemy = enemy;
        }
        else
        {
            _enemy = null;
        }
    }

    private void OnEnable()
    {
        _player.VampAttack += StartVampirism;
    }

    private void OnDisable()
    {
        _player.VampAttack -= StartVampirism;
    }

    private void SuckBlood(EnemyFightController enemy)
    {
        enemy.TakeDamage(_vampireDamage);
        _player.Heal(_vampireHeal);
    }

    private IEnumerator Vampirism()
    {
        for (int i = 0; i < _vampirismDuration; i++)
        {
            if (_enemy == null)
            {
                yield break;
            }

            SuckBlood(_enemy);
            yield return _suckDelay;
        }
    }

    private void StartVampirism()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Vampirism());
    }
}
