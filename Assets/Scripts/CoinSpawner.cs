using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private Coin _coin;
    
    private Transform _target;
    private Transform[] _points;
    
    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
            _target = _points[i].transform;
            Instantiate(_coin, _target.transform.position, Quaternion.identity);
        }
    }
}
