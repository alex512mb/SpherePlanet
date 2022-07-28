using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStarNavAgent))]
public class Player : MonoBehaviour
{
    [SerializeField] Animation _animation;
    private AStarNavAgent navAgent;


    private void Awake()
    {
        navAgent = GetComponent<AStarNavAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animation.Play("Jump");
        }
        if (!_animation.isPlaying)
        {
            _animation.Play("Idle");
        }
    }

    public void GoTo(Vector3 point)
    {
        navAgent.GoTo(point);
    }
}
