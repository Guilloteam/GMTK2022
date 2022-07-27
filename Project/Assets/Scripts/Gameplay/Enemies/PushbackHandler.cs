using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushbackHandler : MonoBehaviour
{
    public float maxForce = 10;
    public float forceRatio = 1;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void HandlePushback(Vector3 direction)
    {
        float intensity = direction.magnitude * forceRatio;
        rigidbody.AddForce(direction.normalized * Mathf.Clamp(intensity, 0, maxForce));
    }
}
