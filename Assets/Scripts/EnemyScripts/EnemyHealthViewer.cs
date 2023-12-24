using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthViewer : MonoBehaviour
{
    [SerializeField] private Slider _sliderHealthPoints;
    [SerializeField] private float _healPointChangeSpeed;

    private EnemyFightController _enemy;
    private Coroutine _coroutine;
    private Vector3 _offset = new Vector3(0, 1);

    private void Awake()
    {
        _enemy = GetComponent<EnemyFightController>();
        _sliderHealthPoints.maxValue = _enemy.MaxHealth;
    }

    private void OnEnable()
    {
        _enemy.HealthChanged += StartDisplayHealth;
    }


    private void OnDisable()
    {
        _enemy.HealthChanged -= StartDisplayHealth;
    }

    private void Update()
    {
        _sliderHealthPoints.transform.position = Camera.main.WorldToScreenPoint(_enemy.transform.position + _offset);
    }

    public void StartDisplayHealth()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(DisplayHealth());
    }

    private IEnumerator DisplayHealth()
    {
        while (_sliderHealthPoints.value != _enemy.CurrentHealthPoints)
        {
            _sliderHealthPoints.value = Mathf.MoveTowards(_sliderHealthPoints.value, _enemy.CurrentHealthPoints, _healPointChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
