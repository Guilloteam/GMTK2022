using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabHand : MonoBehaviour
{
    public static GrabHand instance;
    private List<Grabbable> inRangeElements = new List<Grabbable>();
    public Grabbable grabbedElement;
    private bool inRangeListChanged = false;
    private Grabbable closestGrabbableInRange;
    public Transform grabPosition;
    public InputAction grabAction;
    public float grabAnimDuration = 0.3f;
    public LayerMask raycastLayerMask;
    public System.Action grabStartedDelegate;
    public System.Action<Vector3> throwDelegate;
    public bool canGrab = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        grabAction.Enable();
    }

    void Update()
    {
        if(inRangeListChanged)
        {
            inRangeListChanged = true;
            float closestDistanceSquared = Mathf.Infinity;
            int closestIndex = -1;
            for(int i=0; i<inRangeElements.Count;i++)
            {
                float distanceSquared = Vector3.SqrMagnitude(transform.position - inRangeElements[i].transform.position);
                if(distanceSquared < closestDistanceSquared)
                {
                    closestDistanceSquared = distanceSquared;
                    closestIndex = i;
                }
            }
            Grabbable newClosestGrabbable = closestIndex >= 0 ? inRangeElements[closestIndex] : null;
            if(newClosestGrabbable != closestGrabbableInRange)
            {
                if(newClosestGrabbable != null && grabbedElement == null)
                    newClosestGrabbable.hoverStartDelegate?.Invoke();
                if(closestGrabbableInRange != null)
                    closestGrabbableInRange.hoverEndDelegate?.Invoke();
                closestGrabbableInRange = newClosestGrabbable;
            }
        }
        if(grabAction.WasPressedThisFrame() && canGrab)
        {
            if(grabbedElement == null)
            {
                if(closestGrabbableInRange != null)
                {
                    closestGrabbableInRange.hoverEndDelegate?.Invoke();
                    grabbedElement = closestGrabbableInRange;
                    inRangeElements.Remove(grabbedElement);
                    inRangeListChanged = true;
                    grabStartedDelegate?.Invoke();
                    StartCoroutine(GrabAnimationCoroutine(grabAnimDuration));
                }
            }
            else
            {
                grabbedElement.grabbed = false;
                RaycastHit hit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit, 1000, raycastLayerMask))
                {
                    Vector3 throwDirection = hit.point - grabPosition.position;
                    throwDirection.y = 0;
                    grabbedElement.Throw(throwDirection.normalized);
                    grabbedElement = null;
                    throwDelegate?.Invoke(throwDirection.normalized);
                    if(closestGrabbableInRange != null)
                        closestGrabbableInRange.hoverStartDelegate?.Invoke();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Grabbable grabbable = other.GetComponentInParent<Grabbable>();
        if(grabbable != null)
        {
            inRangeElements.Add(grabbable);
            inRangeListChanged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Grabbable grabbable = other.GetComponentInParent<Grabbable>();
        if(grabbable != null)
        {
            inRangeElements.Remove(grabbable);
            inRangeListChanged = true;
        }
    }

    public void ForceGrab(Grabbable toGrab)
    {
        if(closestGrabbableInRange != null)
        {
            closestGrabbableInRange.hoverEndDelegate?.Invoke();
        }
        grabbedElement = toGrab;
        inRangeElements.Remove(grabbedElement);
        inRangeListChanged = true;
        StartCoroutine(GrabAnimationCoroutine(0));
    }
    
    private IEnumerator GrabAnimationCoroutine(float duration)
    {
        grabbedElement.grabbedDelegate?.Invoke();
        grabbedElement.grabbed = true;
        Vector3 startOffset = grabbedElement.transform.position - grabPosition.transform.position;
        for(float time=0; time < duration; time += Time.deltaTime)
        {
            if(grabbedElement == null)
                break;
            grabbedElement.transform.position = grabPosition.transform.position + startOffset * (1 - time / duration);
            yield return null;
        }
        while(grabbedElement != null)
        {
            grabbedElement.transform.position = grabPosition.transform.position;
            yield return null;
        }
    }
}
