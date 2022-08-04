using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    private MovementController movementController;
    private MeshRenderer meshRenderer;
    public Transform target;
    public float angleOffsetMin = 0;
    public float angleOffsetMax = 0;
    private float angleOffset;
    
    void Start()
    {
        movementController = GetComponent<MovementController>();
        target = KeyboardMovement.instance.transform;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        angleOffset = Random.Range(angleOffsetMin, angleOffsetMax);
    }

    void Update()
    {
        Vector3 direction = Quaternion.AngleAxis(angleOffset, Vector3.up) * (target.position - transform.position);
        direction.y = 0;
        movementController.inputDirection = direction.normalized;
        
        
        if(direction.x < 0)
            meshRenderer.transform.rotation = Quaternion.Euler(90, 0, 0);
        else meshRenderer.transform.rotation = Quaternion.Euler(-90, 0, 180);
    }
}
