using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMover _mover;

    private int Speed = Animator.StringToHash("Speed");
    private int Chop = Animator.StringToHash("Attack");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<PlayerMover>();
    }

    public void AttackAnimation()
    {
        _animator.Play(Chop);
    }
    
    public void WalkAnimation()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_mover.MoveSpeed));
    }
}
