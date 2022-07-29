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
    private int hoveredSlotIndex;
    public System.Action faceHoverStartDelegate;
    public System.Action faceHoverEndDelegate;
    public System.Action faceClickDelegate;
    public System.Action<DiceSlot, int> diceSlotClickedDelegate;
    private List<Coroutine> pendingCoroutines = new List<Coroutine>();
    private DiceSlots slots;
    private float turnStart = 0;
    private float turnTarget = 0;
    private Quaternion startRotation;
    private float turnValue = 0;
    private float turnTime = 0;
    void Start()
    {
        slots = GetComponent<DiceSlots>();
        
        startRotation = transform.rotation;
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit, 100, raycastLayer))
        {
            DiceSlot slot = hit.collider.GetComponent<DiceSlot>();
            if(slot != null && slot.transform.parent == transform)
            {
                if(hoveredSlotIndex >= 0 && hoveredSlotIndex != slot.slotIndex)
                {
                    slots.slots[hoveredSlotIndex].hovered = false;
                    faceHoverEndDelegate?.Invoke();
                }
                slot.hovered = true;
                faceHoverStartDelegate?.Invoke();
                hoveredSlotIndex = slot.slotIndex;
                if(Mouse.current.leftButton.wasPressedThisFrame)
                {
                    diceSlotClickedDelegate?.Invoke(slots.slots[hoveredSlotIndex], hoveredSlotIndex);
                    faceClickDelegate?.Invoke();
                }
            }
        }
        else
        {
            if(hoveredSlotIndex >= 0)
            {
                slots.slots[hoveredSlotIndex].hovered = false;
                faceHoverEndDelegate?.Invoke();
            }
            hoveredSlotIndex = -1;
        }
        UpdateTurn();
    }

    public void Turn()
    {
        turnTime = 0;
        turnTarget += 1;
        turnStart = turnValue;
    }

    public void UpdateTurn()
    {
        Physics.autoSimulation = false;
        
        if(turnTime < turnDuration)
        {
            turnTime += Time.unscaledDeltaTime;
            float ratio = turnTime / turnDuration;
            ratio = 1 - (1-ratio) * (1 - ratio);
            turnValue = Mathf.Lerp(turnStart, turnTarget, ratio);
        }
        transform.rotation = startRotation * Quaternion.AngleAxis(180 * turnValue, rotationAxis);
    }
}
