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
    private float activableDelay = 0;
    public float throwActivationDelay = 0.3f;
    private bool grabbed = false;
    

    void Start()
    {
        projectilePhysics = GetComponent<ProjectilePhysics>();
        projectilePhysics.physicsConfig = idlePhysicsConfig;
        projectilePhysics.returnToIdleStateDelegate += OnReturnToIdleState;
        grabbable = GetComponent<Grabbable>();
        grabbable.throwDelegate += OnThrow;
        grabbable.grabbedDelegate += OnGrabbed;
        diceSlots = GetComponent<DiceSlots>();
    }

    void Update()
    {
        activableDelay -= Time.deltaTime;
    }

    private void OnThrow(Vector3 direction)
    {
        projectilePhysics.physicsConfig = thrownPhysicsConfig;
        activeFace = -1;
        grabbed = false;
        activableDelay = throwActivationDelay;
    }

    private void OnGrabbed()
    {
        grabbed = true;
        if(activeFace >= 0)
            diceSlots.slots[activeFace].activationEndDelegate?.Invoke();
    }

    private void OnReturnToIdleState()
    {
        if(grabbed || activableDelay > 0)
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
                diceSlots.slots[activeFace].activationEndDelegate?.Invoke();
            diceSlots.slots[bestFace].activationStartDelegate?.Invoke();
            activeFace = bestFace;
        }
    }
}
