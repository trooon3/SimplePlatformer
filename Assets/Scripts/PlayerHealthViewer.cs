using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _sliderHealthPoints;
    [SerializeField] private float _healPointChangeSpeed;

    private Coroutine _coroutine;
    private Vector3 _offset = new Vector3(0, 1);

    private void Start()
    {
        _sliderHealthPoints.maxValue = _player.MaxHealth;
    }

    private void OnEnable()
    {
        _player.HealthChanged += StartDisplayHealth;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= StartDisplayHealth;
    }

    private void Update()
    {
        _sliderHealthPoints.transform.position = Camera.main.WorldToScreenPoint(_player.transform.position + _offset);
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
        while (_sliderHealthPoints.value != _player.CurrentHealthPoints)
        {
            _sliderHealthPoints.value = Mathf.MoveTowards(_sliderHealthPoints.value, _player.CurrentHealthPoints, _healPointChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}

