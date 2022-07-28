using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMoveDirection : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 90;
    private Vector3 lastPosition;

    private Quaternion targetRotation;


    private void Awake()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (transform.position == lastPosition)
            return;

        //y axis along direction from center of world
        Vector3 directionFromCenter = transform.position.normalized;

        Vector3 vectorMove = (transform.position - lastPosition).normalized;

        targetRotation = Quaternion.LookRotation(vectorMove, directionFromCenter);

        lastPosition = transform.position;
    }
}
