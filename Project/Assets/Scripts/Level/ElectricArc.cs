using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ElectricArc : MonoBehaviour
{
    public GameObject origin;
    public GameObject target;
    public LineRenderer lineRenderer;
    void Start()
    {
        transform.position = origin.transform.position;
        for(int i=0; i<lineRenderer.positionCount; i++)
        {
            float ratio = (float)i / lineRenderer.positionCount;
            lineRenderer.SetPosition(i, transform.position * ratio + target.transform.position * (1 - ratio));
        }
    }

    void Update()
    {
        transform.position = origin.transform.position;
        for(int i=0; i<lineRenderer.positionCount; i++)
        {
            float ratio = (float)i / lineRenderer.positionCount;
            lineRenderer.SetPosition(i, transform.position * ratio + target.transform.position * (1 - ratio));
        }
    }
}
