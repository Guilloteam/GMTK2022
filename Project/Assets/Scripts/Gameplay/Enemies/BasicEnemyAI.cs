using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    private MovementController movementController;
    private MeshRenderer meshRenderer;
    public float movementSpeed = 1;
    public Transform target;
    
    void Start()
    {
        movementController = GetComponent<MovementController>();
        target = KeyboardMovement.instance.transform;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position);
        direction.y = 0;
        movementController.inputDirection = direction.normalized;
        
        
        if(direction.x < 0)
            meshRenderer.transform.rotation = Quaternion.Euler(90, 0, 0);
        else meshRenderer.transform.rotation = Quaternion.Euler(-90, 0, 180);
    }
}
