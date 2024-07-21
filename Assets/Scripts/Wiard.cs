using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Wiard : MonoBehaviour
{
    [SerializeField]
    private InputBank inputBank;

    private List<ElementManipulation> _elements = new List<ElementManipulation>();

    private void Start()
    {
        _elements.Add(new ElementManipulation(ElementTypeIndex.FIRE, inputBank.M1));
        _elements.Add(new ElementManipulation(ElementTypeIndex.EARTH, inputBank.M2));
        _elements.Add(new ElementManipulation(ElementTypeIndex.WATER, inputBank.Shift));
        _elements.Add(new ElementManipulation(ElementTypeIndex.AIR, inputBank.Space));
    }

    void Update()
    {
        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i].Update(inputBank);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i].FixedUpdate(inputBank);
        }
    }
}
