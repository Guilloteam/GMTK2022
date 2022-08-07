using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceProjectile : MonoBehaviour
{
    [HideInInspector]
    public ProjectilePhysics projectilePhysics;
    private Grabbable grabbable;
    public ProjectilePhysicsSettings idlePhysicsConfig;
    public ProjectilePhysicsSettings thrownPhysicsConfig;
    private DiceSlots diceSlots;
    private int activeFace = -1;
    private bool grabbed = false;
    private CollisionDamageDealer damageBox;
    

    void Start()
    {
        projectilePhysics = GetComponent<ProjectilePhysics>();
        projectilePhysics.physicsConfig = idlePhysicsConfig;
        projectilePhysics.returnToIdleStateDelegate += OnReturnToIdleState;
        projectilePhysics.leaveIdleStateDelegate += OnLeaveIdleState;
        grabbable = GetComponent<Grabbable>();
        grabbable.throwDelegate += OnThrow;
        grabbable.grabbedDelegate += OnGrabbed;
        diceSlots = GetComponent<DiceSlots>();
        damageBox = GetComponentInChildren<CollisionDamageDealer>();
    }

    void Update()
    {
    }

    private void OnThrow(Vector3 direction)
    {
        if(activeFace >= 0)
            diceSlots.slots[activeFace].thrownDelegate?.Invoke();
        projectilePhysics.physicsConfig = thrownPhysicsConfig;
        activeFace = -1;
        grabbed = false;
        damageBox.enabled = true;
    }

    private void OnGrabbed()
    {
        grabbed = true;
        if(activeFace >= 0)
            diceSlots.slots[activeFace].grabbedDelegate?.Invoke();
    }

    private void OnReturnToIdleState()
    {
        damageBox.enabled = false;
        damageBox.ResetHurtTargets();
        if(grabbed)
            return;
        projectilePhysics.physicsConfig = idlePhysicsConfig;
        float maxValue = 0;
        int bestFace = -1;
        for(int i=0; i<diceSlots.slots.Length; i++)
        {
            float dot = Vector3.Dot(Vector3.up, diceSlots.slots[i].transform.up);
            if(dot > maxValue)
            {
                bestFace = i;
                maxValue = dot;
            }
        }
        if(activeFace != bestFace)
        {
            if(activeFace >= 0)
                diceSlots.slots[activeFace].activeFaceTurnedDelegate?.Invoke();
            diceSlots.slots[bestFace].activationStartDelegate?.Invoke();
            activeFace = bestFace;
        }
    }

    private void OnLeaveIdleState()
    {
        damageBox.enabled = true;
        if(activeFace >= 0)
            diceSlots.slots[activeFace].activeFaceTurnedDelegate?.Invoke();
        activeFace = -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION DICE/" + collision.collider.gameObject);
    }
}
