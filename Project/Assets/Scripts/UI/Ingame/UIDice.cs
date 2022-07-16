using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIDice : MonoBehaviour
{
    public new Camera camera;
    public LayerMask raycastLayer;
    public float turnDuration;
    public Vector3 rotationAxis = (Vector3.right + Vector3.forward)/2;
    private DiceSlot hoveredSlot;
    public System.Action<DiceSlot> diceSlotClickedDelegate;
    void Start()
    {
        
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit, 100, raycastLayer))
        {
            DiceSlot slot = hit.collider.GetComponent<DiceSlot>();
            if(slot != null)
            {
                if(hoveredSlot != null && hoveredSlot != slot)
                    hoveredSlot.hovered = false;
                slot.hovered = true;
                hoveredSlot = slot;
                if(Mouse.current.leftButton.wasPressedThisFrame)
                {
                    diceSlotClickedDelegate?.Invoke(hoveredSlot);
                }
            }
        }
        else
        {
            if(hoveredSlot != null)
                hoveredSlot.hovered = false;
            hoveredSlot = null;
        }
    }

    public void Turn()
    {
        StartCoroutine(TurnCoroutine());
    }

    private IEnumerator TurnCoroutine()
    {
        Quaternion startRotation = transform.rotation;
        
        for(float time = 0; time < turnDuration; time += Time.deltaTime)
        {
            float ratio = time / turnDuration;
            ratio = 1 - (1-ratio) * (1 - ratio);
            transform.rotation = startRotation * Quaternion.AngleAxis(180 * ratio, rotationAxis);
            yield return null;
        }
        transform.rotation = startRotation * Quaternion.AngleAxis(180, rotationAxis);
    }
}
