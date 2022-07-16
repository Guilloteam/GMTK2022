using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    private MovementController movementController;
    public float movementSpeed = 1;
    public Transform target;
    
    void Start()
    {
        movementController = GetComponent<MovementController>();
        target = KeyboardMovement.instance.transform;
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position);
        direction.y = 0;
        movementController.inputDirection = direction.normalized;
    }
}
