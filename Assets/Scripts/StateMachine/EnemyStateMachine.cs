using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFightController))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private State _currentState;
    private Player _target;

    public State CurrentState => _currentState;

    private void Start()
    {
        _target = GetComponent<EnemyFightController>().Target;
        Reset(_startState);
    }

    private void Update()
    {
        if (_currentState == null)
          return;
        
        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();
       
        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }
}
