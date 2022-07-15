using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceProjectile : MonoBehaviour
{
    public ProjectilePhysics projectilePhysics;
    private Grabbable grabbable;
    public ProjectilePhysicsSettings idlePhysicsConfig;
    public ProjectilePhysicsSettings thrownPhysicsConfig;

    void Start()
    {
        projectilePhysics = GetComponent<ProjectilePhysics>();
        projectilePhysics.physicsConfig = idlePhysicsConfig;
        projectilePhysics.returnToIdleStateDelegate += OnReturnToIdleState;
        grabbable = GetComponent<Grabbable>();
        grabbable.throwDelegate += OnThrow;
    }

    void Update()
    {
        
    }

    private void OnThrow(Vector3 direction)
    {
        projectilePhysics.physicsConfig = thrownPhysicsConfig;
    }

    private void OnReturnToIdleState()
    {
        projectilePhysics.physicsConfig = idlePhysicsConfig;
    }
}
