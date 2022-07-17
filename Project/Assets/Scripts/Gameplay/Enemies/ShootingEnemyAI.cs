using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyAI : MonoBehaviour
{
    private MovementController movementController;
    public float movementSpeed = 1;
    public Transform target;
    public float shootingMinRange = 5;
    public float shootingMaxRange = 10;
    public float loadingDuration = 3;
    private float loadingTime = 0;

    public Rigidbody projectilePrefab;
    public float projectileSpeed = 1;
    
    void Start()
    {
        movementController = GetComponent<MovementController>();
        target = KeyboardMovement.instance.transform;
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position);
        direction.y = 0;
        if(direction.sqrMagnitude < shootingMinRange * shootingMinRange)
        {
            movementController.inputDirection = -direction.normalized;
            loadingTime = 0;
        }
        else if(direction.sqrMagnitude > shootingMaxRange * shootingMaxRange)
        {
            movementController.inputDirection = direction.normalized;
            loadingTime = 0;
        }
        else
        {
            movementController.inputDirection = Vector3.zero;
            loadingTime += Time.deltaTime;
            if(loadingTime > loadingDuration)
            {
                Instantiate(projectilePrefab, transform.position, projectilePrefab.rotation).velocity = direction.normalized * projectileSpeed;
                loadingTime = 0;
            }
        }
    }
}
