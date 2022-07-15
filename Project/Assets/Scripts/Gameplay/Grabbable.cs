using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public new Rigidbody rigidbody;
    private bool _hovered;
    public System.Action hoverStartDelegate;
    public System.Action hoverEndDelegate;

    private Collider[] colliders;
    private Collider mainCollider;

    public bool hovered { 
        get {
            return _hovered;
        }
        set {
            if(value != _hovered)
            {
                if(value)
                    hoverStartDelegate?.Invoke();
                else hoverEndDelegate?.Invoke();
                _hovered = value;
            }
        }
    }

    private bool _grabbed;

    public bool grabbed {
        get {
            return _grabbed;
        }

        set {
            rigidbody.isKinematic = value;
            _grabbed = value;
            mainCollider.enabled = !value;
            foreach(Collider collider in colliders)
            {
                collider.enabled = !value;
            }
        }
    }

    public void Throw(Vector3 direction, float force, float torque)
    {
        rigidbody.velocity = direction * force;
        rigidbody.AddTorque(Vector3.Cross(direction, Vector3.up) * torque, ForceMode.Acceleration);
    }

    public void Start()
    {
        mainCollider = GetComponent<Collider>();
        colliders = GetComponentsInChildren<Collider>();
    }
}
