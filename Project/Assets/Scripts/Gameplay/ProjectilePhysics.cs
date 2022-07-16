using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePhysics : MonoBehaviour
{
    public ProjectilePhysicsSettings physicsConfig;
    private new Rigidbody rigidbody;
    private Grabbable grabbable;
    public int sampleCount = 10;
    private float[] movementSamples;
    private int movementSampleCursor = 0;
    public float immobileThreshold = 1;
    public bool immobile = false;
    public float angularSpeedThresholdMultiplier = 10;
    public System.Action returnToIdleStateDelegate;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();
        grabbable.throwDelegate += OnThrow;
        movementSamples = new float[sampleCount];
    }

    private void FixedUpdate()
    {
        movementSamples[(movementSampleCursor++) % movementSamples.Length] = Mathf.Max(rigidbody.velocity.sqrMagnitude, rigidbody.angularVelocity.sqrMagnitude * angularSpeedThresholdMultiplier);
        float sum = 0;
        for(int i=0; i<movementSamples.Length; i++)
        {
            sum += movementSamples[i];
        }
        bool wasImmobile = immobile;
        immobile = sum < immobileThreshold;
        if(!wasImmobile && immobile)
        {
            returnToIdleStateDelegate?.Invoke();
        }
        rigidbody.mass = physicsConfig.mass;
        rigidbody.velocity = Vector3.Scale(rigidbody.velocity, new Vector3(Mathf.Pow(physicsConfig.drag.x, Time.fixedDeltaTime), Mathf.Pow(physicsConfig.drag.y, Time.fixedDeltaTime), Mathf.Pow(physicsConfig.drag.z, Time.fixedDeltaTime))) ;
        rigidbody.velocity += physicsConfig.gravity * Time.fixedDeltaTime;
    }

    private void OnThrow(Vector3 direction)
    {
        rigidbody.velocity = direction * physicsConfig.throwStrength.x + Vector3.up * physicsConfig.throwStrength.y;
        rigidbody.maxAngularVelocity = physicsConfig.throwTorque;
        rigidbody.AddTorque(Vector3.Cross(direction, Vector3.up).normalized * physicsConfig.throwTorque, ForceMode.VelocityChange);
    }
}
