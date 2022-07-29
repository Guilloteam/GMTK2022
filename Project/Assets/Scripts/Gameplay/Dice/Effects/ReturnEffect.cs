using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEffect : MonoBehaviour
{
    private Grabbable grabbable;
    public Transform fxPrefab;
    public float disappearDelay = 0.2f;
    public System.Action diceDisappearDelegate;
    public System.Action diceReappearDelegate;
    private List<Coroutine> coroutines = new List<Coroutine>();
    private bool grabDisabled = false;
    private IEnumerator Start()
    {
        DiceBuilder diceBuilder = GetComponent<DiceEffectRoot>().dice;
        grabbable = diceBuilder.GetComponent<Grabbable>();
        yield return ReturnAnimCoroutine();
    }

    private void OnDestroy()
    {
        if(grabDisabled)
            GrabHand.instance.canGrab = true;
    }

    private IEnumerator ReturnAnimCoroutine()
    {
        while(!grabbable || GrabHand.instance.grabbedElement != null || !GrabHand.instance.canGrab)
            yield return null;
        GrabHand.instance.canGrab = false;
        grabDisabled = true;
        Instantiate(fxPrefab, grabbable.transform.position, fxPrefab.rotation);
        diceDisappearDelegate?.Invoke();
        yield return new WaitForSeconds(disappearDelay);
        
        GrabHand.instance.ForceGrab(grabbable);
        Instantiate(fxPrefab, grabbable.transform.position, fxPrefab.rotation);
        diceReappearDelegate?.Invoke();
        GrabHand.instance.canGrab = true;
        grabDisabled = false;
    }
}
