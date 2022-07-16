using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    private new Rigidbody2D rigidbody;
    public float force = 50;
    public float closeRange = 5;
    public float drag = 0.01f;
    private Vector3 velocity;


    void Start()
    {
        targetOffset = transform.position - target.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.z = 0;
        velocity = velocity * Mathf.Pow(drag, Time.deltaTime) + direction.normalized * Mathf.Min(direction.magnitude / closeRange, 1) * force * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    void FixedUpdate()
    {

    }
}
