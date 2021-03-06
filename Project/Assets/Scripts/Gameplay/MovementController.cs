using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public EntityPhysicsConfig physicsConfig;
    public Vector3 inputDirection;
    private new Rigidbody rigidbody;
    public static MovementController instance;



    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        rigidbody.mass = physicsConfig.mass;
        rigidbody.AddForce(inputDirection * physicsConfig.acceleration + Vector3.down * physicsConfig.gravity, ForceMode.Acceleration);
        rigidbody.velocity = rigidbody.velocity * Mathf.Pow(physicsConfig.inertia, Time.fixedDeltaTime);
        if(rigidbody.velocity.sqrMagnitude > physicsConfig.maxSpeed * physicsConfig.maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity * physicsConfig.maxSpeed / rigidbody.velocity.magnitude;
        }
    }
}
