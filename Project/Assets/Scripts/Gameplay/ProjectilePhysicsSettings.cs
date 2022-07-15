using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Physics/Projectile Physics")]
public class ProjectilePhysicsSettings : ScriptableObject
{
    public Vector2 throwStrength;
    public float throwTorque;
    public Vector3 drag;
    public float mass;
    public Vector3 gravity;
}
