using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileState
{
    Idle,
    Moving
}

public class ProjectilePhysics : MonoBehaviour
{
    public ProjectilePhysicsSettings physicsConfig;
    private new Rigidbody rigidbody;
    private Grabbable grabbable;
    public int sampleCount = 10;
    private float[] movementSamples;
    private int movementSampleCursor = 0;
    ProjectileState currentState;
    public float alignmentThreshold = 0.01f;
    public float angularSpeedThreshold = 0.05f;
    public System.Action returnToIdleStateDelegate;
    public System.Action leaveIdleStateDelegate;
    public float leaveIdlestateThreshold = 0.5f;
    private int verticalDirectionIndex;
    public float immobileThreshold = 1;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();
        grabbable.throwDelegate += OnThrow;
        
        movementSamples = new float[sampleCount];
    }
    

    private void FixedUpdate()
    {
        Vector3[] directionsArray = new Vector3[]{transform.right, transform.up, transform.forward};
        switch(currentState)
        {
            case ProjectileState.Idle:
                float verticalDirection = directionsArray[verticalDirectionIndex].y;
                if(Mathf.Abs(verticalDirection) < leaveIdlestateThreshold)
                {
                    currentState = ProjectileState.Moving;
                    leaveIdleStateDelegate?.Invoke();
                }
            break;
            case ProjectileState.Moving:
                
                if(rigidbody.velocity.sqrMagnitude < immobileThreshold * immobileThreshold && rigidbody.angularVelocity.sqrMagnitude < angularSpeedThreshold * angularSpeedThreshold)
                {
                    for(int i=0; i<directionsArray.Length; i++)
                    {
                        if(Mathf.Abs(directionsArray[i].y) > 1-alignmentThreshold)
                        {
                            currentState = ProjectileState.Idle;
                            returnToIdleStateDelegate?.Invoke();
                            verticalDirectionIndex = i;
                            break;
                        }
                    }
                }

                
            break;
        }
        rigidbody.mass = physicsConfig.mass;
        rigidbody.velocity = Vector3.Scale(rigidbody.velocity, new Vector3(Mathf.Pow(physicsConfig.drag.x, Time.fixedDeltaTime), Mathf.Pow(physicsConfig.drag.y, Time.fixedDeltaTime), Mathf.Pow(physicsConfig.drag.z, Time.fixedDeltaTime))) ;
        rigidbody.velocity += physicsConfig.gravity * Time.fixedDeltaTime;
    }

    private void OnThrow(Vector3 direction)
    {
        currentState = ProjectileState.Moving;
        rigidbody.velocity = direction * physicsConfig.throwStrength.x + Vector3.up * physicsConfig.throwStrength.y;
        rigidbody.maxAngularVelocity = physicsConfig.throwTorque + physicsConfig.throwTorqueVariation;
        rigidbody.AddTorque(Vector3.Cross(direction, Vector3.up).normalized * (physicsConfig.throwTorque + Random.Range(-physicsConfig.throwTorqueVariation, physicsConfig.throwTorqueVariation)), ForceMode.VelocityChange);
    }
}
