using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonus : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    public int bonus;
    public Vector3 velocity;
    private Color color;
    public float fadeDuration = 3;

    IEnumerator Start()
    {
        text.text = "+" + bonus;
        color = text.color;
        for(float time = 0; time < fadeDuration; time+= Time.deltaTime)
        {
            transform.position += velocity * Time.deltaTime;
            color.a = 1 - time / fadeDuration;
            text.color = color;
            yield return null;
        }
    }
}
