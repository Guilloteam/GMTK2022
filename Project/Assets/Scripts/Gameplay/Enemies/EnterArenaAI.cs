using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArenaAI : MonoBehaviour
{
    private MovementController movementController;
    private MeshRenderer meshRenderer;
    public float arenaSize = 10;
    public MonoBehaviour nextAIScript;
    public Vector3 arenaCenter = Vector3.zero;

    
    void Start()
    {
        movementController = GetComponent<MovementController>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        nextAIScript.enabled = false;
    }

    void Update()
    {
        Vector3 direction = (arenaCenter - transform.position);
        direction.y = 0;

        movementController.inputDirection = direction.normalized;
        
        
        if(direction.x < 0)
            meshRenderer.transform.rotation = Quaternion.Euler(90, 0, 0);
        else meshRenderer.transform.rotation = Quaternion.Euler(-90, 0, 180);

        if(direction.sqrMagnitude < arenaSize * arenaSize)
        {
            nextAIScript.enabled = true;
            enabled = false;
        }
    }
}
