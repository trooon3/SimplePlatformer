using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedSpawner : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _hospital;
    [SerializeField] private MedicalKit _medicalKit;


    private void Start()
    {
        _points = new Transform[_hospital.childCount];

        for (int i = 0; i < _hospital.childCount; i++)
        {
            _points[i] = _hospital.GetChild(i);
        }

        for (int i = 0; i < _count; i++)
        {
            Instantiate(_medicalKit, _points[Random.Range(0, _points.Length)]);
        }
        
    }

}
