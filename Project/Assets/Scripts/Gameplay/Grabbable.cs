using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public new Rigidbody rigidbody;
    private bool _hovered;
    public System.Action hoverStartDelegate;
    public System.Action hoverEndDelegate;
    public System.Action<Vector3> throwDelegate;
    public System.Action grabbedDelegate;

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

    public void Throw(Vector3 direction)
    {
        throwDelegate?.Invoke(direction);
        
    }

    public void Start()
    {
        mainCollider = GetComponent<Collider>();
        colliders = GetComponentsInChildren<Collider>();
    }
}
