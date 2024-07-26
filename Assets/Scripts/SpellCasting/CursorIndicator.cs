using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIndicator : MonoBehaviour
{
    [SerializeField]
    private Color color;
    public Color Color { 
        get =>  color; 
        set
        {
            if (color == value) 
                return;
            color = value;
            UpdateParticleColors();
        } 
    }

    [SerializeField]
    private ParticleSystem[] particleSystems;

    private ParticleSystem.MainModule[] mainModules;

    private void Awake()
    {
        mainModules = new ParticleSystem.MainModule[particleSystems.Length];
        for (int i = 0; i < particleSystems.Length; i++)
        {
            mainModules[i] = particleSystems[i].main;
        }
        UpdateParticleColors();
    }


    private void UpdateParticleColors()
    {
        for (int i = 0; i< mainModules.Length; i++)
        {
            mainModules[i].startColor = color;
        }
    }
}
