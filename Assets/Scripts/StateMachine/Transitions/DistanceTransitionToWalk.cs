using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransitionToWalk : Transition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > _transitionRange || Target == null)
            NeedTransit = true;
    }
}
