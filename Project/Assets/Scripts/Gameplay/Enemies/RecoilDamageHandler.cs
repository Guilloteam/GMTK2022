using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilDamageHandler : MonoBehaviour
{
    public float recoilRatio = 1;
    private new Rigidbody rigidbody;
    private MovementController movementController;
    public EntityPhysicsConfig knockedPhysics;
    private EntityPhysicsConfig defaultPhysics;
    private float knockedTime;
    public float knockedDuration = 1;
    public float maxForce = 10;
    public float forceRatio = 1;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        movementController = GetComponentInParent<MovementController>();
        DamageReceiver damageReceiver = GetComponentInParent<DamageReceiver>();
        defaultPhysics = movementController.physicsConfig;
    }

    public void OnPushback(Vector3 forceApplied)
    {
        
        Vector3 direction = forceApplied;
        direction.y = 0;
        float intensity = direction.magnitude * forceRatio;
        rigidbody.AddForce(direction.normalized * Mathf.Clamp(intensity, 0, maxForce));
        // rigidbody.AddForce(forceApplied * recoilRatio);
        movementController.physicsConfig = knockedPhysics;
        knockedTime = knockedDuration;
    }

    private void Update()
    {
        if(knockedTime > 0)
        {
            knockedTime -= Time.deltaTime;
            if(knockedTime <= 0)
            {
                movementController.physicsConfig = defaultPhysics;
            }
        }
    }

    
}
