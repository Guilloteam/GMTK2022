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
    void Start()
    {
        DiceBuilder diceBuilder = GetComponent<DiceEffectRoot>().dice;
        grabbable = diceBuilder.GetComponent<Grabbable>();
        StartCoroutine(ReturnAnimCoroutine());
    }

    private IEnumerator ReturnAnimCoroutine()
    {
        while(!grabbable || GrabHand.instance.grabbedElement != null)
            yield return null;
        Instantiate(fxPrefab, grabbable.transform.position, fxPrefab.rotation);
        diceDisappearDelegate?.Invoke();
        yield return new WaitForSeconds(disappearDelay);
        
        GrabHand.instance.ForceGrab(grabbable);
        Instantiate(fxPrefab, grabbable.transform.position, fxPrefab.rotation);
        diceReappearDelegate?.Invoke();
    }
}
