using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyAI : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private MovementController movementController;
    public float movementSpeed = 1;
    public Transform target;
    public float shootingMinRange = 5;
    public float shootingMaxRange = 10;
    public float loadingDuration = 3;
    private float loadingTime = 0;

    public Rigidbody projectilePrefab;
    public float projectileSpeed = 1;
    public Transform squashAnimTarget;
    public float maxSquash = 0.7f;
    public Material defaultMaterial;
    public Material shootMaterial;
    public float shootMaterialAppearDuration = 0.5f;
    private float shootMaterialAppearTime = 0;
    
    void Start()
    {
        movementController = GetComponent<MovementController>();
        target = KeyboardMovement.instance.transform;
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position);
        direction.y = 0;
        if(shootMaterialAppearTime > 0)
        {
            shootMaterialAppearTime -= Time.deltaTime;
            meshRenderer.sharedMaterial = shootMaterial;
            if(shootMaterialAppearTime < 0)
                meshRenderer.sharedMaterial = defaultMaterial;
        }
        if(direction.sqrMagnitude < shootingMinRange * shootingMinRange)
        {
            squashAnimTarget.localScale = Vector3.one;
            movementController.inputDirection = -direction.normalized;
            loadingTime = 0;
        }
        else if(direction.sqrMagnitude > shootingMaxRange * shootingMaxRange)
        {
            squashAnimTarget.localScale = Vector3.one;
            movementController.inputDirection = direction.normalized;
            loadingTime = 0;
        }
        else
        {
            movementController.inputDirection = Vector3.zero;
            loadingTime += Time.deltaTime;
            squashAnimTarget.localScale = new Vector3(1, 1, Mathf.Lerp(1, maxSquash, loadingTime / loadingDuration));
            if(loadingTime > loadingDuration)
            {
                squashAnimTarget.localScale = Vector3.one;
                Instantiate(projectilePrefab, transform.position, projectilePrefab.rotation).velocity = direction.normalized * projectileSpeed;
                loadingTime = 0;
                shootMaterialAppearTime = shootMaterialAppearDuration;
            }
        }
    }
}
