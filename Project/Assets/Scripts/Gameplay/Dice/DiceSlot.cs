using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSlot : MonoBehaviour
{
    public int index;
    public System.Action activationStartDelegate;
    public System.Action activationEndDelegate;
    public bool hovered = false;
    public float hoverAnimDuration = 0.2f;
    private float hoverAnimTime = 0;
    public Vector3 hoverAnimDisplacement;
    private Vector3 startPosition;
    public float attachAnimDuration = 0.2f;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(hovered)
        {
            hoverAnimTime += Time.deltaTime;
            if(hoverAnimTime > hoverAnimDuration)
                hoverAnimTime = hoverAnimDuration;
        }
        else
        {
            hoverAnimTime -= Time.deltaTime;
            if(hoverAnimTime < 0)
                hoverAnimTime = 0;
        }
        transform.GetChild(0).localPosition = hoverAnimDisplacement * hoverAnimTime / hoverAnimDuration;
    }

    public void AttachElement(Transform effect)
    {
        StartCoroutine(AttachElementCoroutine(effect));
    }

    private IEnumerator AttachElementCoroutine(Transform side)
    {
        side.SetParent(transform, true);
        Vector3 startPosition = side.localPosition;
        for(float time = 0; time < attachAnimDuration; time += Time.deltaTime)
        {
            yield return null;
        }
    }
}
