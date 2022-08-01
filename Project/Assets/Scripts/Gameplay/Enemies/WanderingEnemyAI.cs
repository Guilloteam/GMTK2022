using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemyAI : MonoBehaviour
{
    private MovementController movementController;
    private MeshRenderer meshRenderer;
    public Transform target;
    public float detectionRange = 4;
    public float stopFollowingRange = 5;
    public float stopFollowingDelay = 2;

    private float stopFollowingTime = Mathf.Infinity;

    public float minStepDuration = 0.5f;
    public float maxStepDuration = 3;
    private Vector2 stepDirection;
    private float currentStepDuration;
    public float minStepDelay = 0.3f;
    public float maxStepDelay = 3;
    
    
    IEnumerator Start()
    {
        movementController = GetComponent<MovementController>();
        target = KeyboardMovement.instance.transform;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        yield return StepCoroutine();
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position);
        direction.y = 0;

        if(direction.sqrMagnitude < detectionRange * detectionRange)
        {
            stopFollowingTime = 0;
        }
        else if(direction.sqrMagnitude < stopFollowingRange * stopFollowingRange && stopFollowingTime < stopFollowingDelay)
        {
            stopFollowingTime -= Time.deltaTime;
        }
        else
        {
            stopFollowingTime += Time.deltaTime;
        }
        
        if(direction.x < 0)
            meshRenderer.transform.rotation = Quaternion.Euler(90, 0, 0);
        else meshRenderer.transform.rotation = Quaternion.Euler(-90, 0, 180);
    }

    private IEnumerator StepCoroutine()
    {
        while(true)
        {
            if(stopFollowingTime > stopFollowingDelay)
            {
                Vector2 direction = Random.insideUnitCircle;
                float duration = Random.Range(minStepDuration, maxStepDuration);
                for(float time=0; time < duration && stopFollowingTime > stopFollowingDelay; time += Time.deltaTime)
                {
                    movementController.inputDirection = new Vector3(direction.x, 0, direction.y);
                    yield return null;
                }
                float delay = Random.Range(minStepDelay, maxStepDelay);
                for(float time=0; time < duration && stopFollowingTime > stopFollowingDelay; time += Time.deltaTime)
                {
                    movementController.inputDirection = Vector3.zero;
                    yield return null;
                }
            }
            else
            {
                Vector3 direction = (target.position - transform.position);
                direction.y = 0;
                movementController.inputDirection = direction.normalized;
                yield return null;
            }
        }
    }
}
