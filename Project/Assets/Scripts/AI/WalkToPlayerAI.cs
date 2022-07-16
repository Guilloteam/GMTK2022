using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToPlayerAI : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public float movementForce = 10;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = MovementController.instance.transform.position;
        rigidbody.AddForce(movementForce * (targetPosition - transform.position).normalized);
    }
}
