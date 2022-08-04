using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    public Transform effectPrefab;

    public void Start()
    {
        Instantiate(effectPrefab, transform.position, effectPrefab.rotation);
    }
}
