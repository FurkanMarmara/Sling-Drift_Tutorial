using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = transform.GetComponent<LineRenderer>();
    }

}
