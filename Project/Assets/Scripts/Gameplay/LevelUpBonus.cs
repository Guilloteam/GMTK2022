using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBonus : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    public string message = "Level up !";
    public Vector3 velocity;
    private Color color;
    public float fadeDuration = 3;
    public float endScale = 3;
    private Vector3 startScale;

    IEnumerator Start()
    {
        startScale = transform.localScale;
        text.text = message;
        color = text.color;
        for(float time = 0; time < fadeDuration; time+= Time.deltaTime)
        {
            transform.position += velocity * Time.deltaTime;
            color.a = 1 - time / fadeDuration;
            text.color = color;
            transform.localScale = startScale * Mathf.Lerp(1, endScale, time / fadeDuration);
            yield return null;
        }
    }
}
