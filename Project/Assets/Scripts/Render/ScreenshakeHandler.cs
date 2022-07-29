using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScreenshakeConfig
{
    public float intensity;
    public float duration;
}

public class ScreenshakeEffect
{
    public float time;
    public ScreenshakeConfig config;
}
public class ScreenshakeHandler : MonoBehaviour
{
    public static ScreenshakeHandler instance;
    public float maxIntensity = 1;
    public float maxDisplacement = 0;
    private Vector3 initialPosition;
    public List<ScreenshakeEffect> activeEffects = new List<ScreenshakeEffect>();
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        initialPosition = transform.localPosition;
    }
    
    void Update()
    {
        float intensity = 0; 
        for(int i=activeEffects.Count-1; i>=0; i--)
        {
            if(activeEffects[i].time > activeEffects[i].config.duration)
            {
                activeEffects.RemoveAt(i);
            }
            else
            {
                float ratio = 1 - activeEffects[i].time / activeEffects[i].config.duration;
                intensity += activeEffects[i].config.intensity * ratio;
                activeEffects[i].time += Time.deltaTime;
            }
        }
        if(Time.timeScale > 0)
            transform.localPosition = initialPosition + Random.insideUnitSphere * Mathf.Lerp(0, maxDisplacement, intensity / maxIntensity);
    }

    public void AddEffect(ScreenshakeConfig config)
    {
        ScreenshakeEffect effect = new ScreenshakeEffect();
        effect.time = 0;
        effect.config = config;
        activeEffects.Add(effect);
    }
}
