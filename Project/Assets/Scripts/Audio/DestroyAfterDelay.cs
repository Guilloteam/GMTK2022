using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float duration = 5;
    private float time;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);

    }
}
