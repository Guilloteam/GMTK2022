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
    public System.Action<DiceSlot, int> diceSlotClickedDelegate;
    private List<Coroutine> pendingCoroutines = new List<Coroutine>();
    private DiceSlots slots;
    void Start()
    {
        slots = GetComponent<DiceSlots>();
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
                    slots.slots[hoveredSlotIndex].hovered = false;
                slot.hovered = true;
                hoveredSlotIndex = slot.slotIndex;
                if(Mouse.current.leftButton.wasPressedThisFrame)
                {
                    diceSlotClickedDelegate?.Invoke(slots.slots[hoveredSlotIndex], hoveredSlotIndex);
                }
            }
        }
        else
        {
            if(hoveredSlotIndex >= 0)
                slots.slots[hoveredSlotIndex].hovered = false;
            hoveredSlotIndex = -1;
        }
    }

    public IEnumerator TurnCoroutine()
    {
        Physics.autoSimulation = false;
        Quaternion startRotation = transform.rotation;
        
        for(float time = 0; time < turnDuration; time += Time.unscaledDeltaTime)
        {
            float ratio = time / turnDuration;
            ratio = 1 - (1-ratio) * (1 - ratio);
            transform.rotation = startRotation * Quaternion.AngleAxis(180 * ratio, rotationAxis);
            yield return null;
        }
        transform.rotation = startRotation * Quaternion.AngleAxis(180, rotationAxis);
        Physics.autoSimulation = true;
    }
}
