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
    public System.Action thrownDelegate;

    private Collider[] colliders;
    private Collider mainCollider;
    int startLayer;

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
            if(value)
            {
                gameObject.layer = LayerMask.NameToLayer("Thrown");
            }
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
        StartCoroutine(ThrownCoroutine(direction));
    }

    public IEnumerator ThrownCoroutine(Vector3 direction)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        throwDelegate?.Invoke(direction);
        thrownDelegate?.Invoke();
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = startLayer;
    }

    public void Start()
    {
        mainCollider = GetComponent<Collider>();
        colliders = GetComponentsInChildren<Collider>();
        startLayer = gameObject.layer;
    }

    public void Update()
    {
        if(_grabbed)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
