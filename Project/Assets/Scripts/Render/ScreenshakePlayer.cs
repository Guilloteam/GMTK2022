using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakePlayer : MonoBehaviour
{
    public ScreenshakeConfig config;
    public bool playOnStart = true;
    void Start()
    {
        if(playOnStart)
        {
            ScreenshakeHandler.instance.AddEffect(config);
        }
    }
    
    public void PlayEffect()
    {
        ScreenshakeHandler.instance.AddEffect(config);
    }
}
