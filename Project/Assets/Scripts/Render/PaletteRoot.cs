using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteRoot : MonoBehaviour
{
    public PaletteConfig startPalette;
    private PaletteConfig _palette;
    public PaletteConfig palette {
        get {
            return _palette;
        }

        set {
            if(_palette != value)
            {
                _palette = value;
                paletteChangedDelegate?.Invoke();
            }
        }
    }
    public System.Action paletteChangedDelegate;
    void Awake()
    {
        if(startPalette != null)
            _palette = startPalette;
    }

    void Update()
    {
        
    }
}
