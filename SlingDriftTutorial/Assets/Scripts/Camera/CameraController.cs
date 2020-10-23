using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;

    private Vector3 myPos;

    private void Start()
    {
        myPos = transform.position;
    }

    void Update()
    {
        Vector3 testVector = _playerTransform.position;
        testVector[1] += 100;
        testVector[2] += -100;
        transform.position = testVector;
    }
}
