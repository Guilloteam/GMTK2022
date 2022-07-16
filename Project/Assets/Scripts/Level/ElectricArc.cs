using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ElectricArc : MonoBehaviour
{
    public Transform target;
    public LineRenderer lineRenderer;
    void Start()
    {
        for(int i=0; i<lineRenderer.positionCount; i++)
        {
            float ratio = (float)i / lineRenderer.positionCount;
            lineRenderer.SetPosition(i, transform.position * ratio + target.position * (1 - ratio));
        }
    }

    void Update()
    {
        for(int i=0; i<lineRenderer.positionCount; i++)
        {
            float ratio = (float)i / lineRenderer.positionCount;
            lineRenderer.SetPosition(i, transform.position * ratio + target.position * (1 - ratio));
        }
    }
}
