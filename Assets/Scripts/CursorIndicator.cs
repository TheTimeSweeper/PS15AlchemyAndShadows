using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIndicator : MonoBehaviour
{
    [SerializeField]
    private Color color;
    public Color Color { get =>  color; set
        {
            color = value;
            mainModule.startColor = color;
        } 
    }

    [SerializeField]
    private ParticleSystem mainSystem;

    private ParticleSystem.MainModule mainModule;

    private void Awake()
    {
        mainModule = mainSystem.main;
        Color = color;
    }
}
