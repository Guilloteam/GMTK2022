using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimStart : MonoBehaviour
{
    public Animator Animator; // Assign in Inspector
    
    public void Start()
    {
        Animator.Play("ANM_basic_ennemy", -1, Random.Range(0.0f, 1.0f));
    }

}

