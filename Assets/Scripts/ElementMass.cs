using ActiveStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMotor : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float acceleration;

    private Vector3 _currentVelocity;
    private Vector3 _desiredVelocity;

    private Vector3 _lastPosition;

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_currentVelocity == Vector3.zero)
            return;
    }
}

public class CharacterMotor : MonoBehaviour
{

}

public class ElementMass : MonoBehaviour
{

    [SerializeField]
    private ActiveStateMachine _activeStateMachine;
    public ActiveStateMachine ActiveStateMachine => _activeStateMachine;

    //JAM originally planned to split the element into equally sized masses based on total mass but shelving for now
    [SerializeField]
    private List<ElementSubMass> subMasses;
    public List<ElementSubMass> SubMasses => subMasses;

    private float _totalMass;
    private Vector3 _centerPosition;
    public Vector3 CenterPosition => _centerPosition;

    public bool Casted { get; set; }

    private Vector3 _lastPosition;
    private Vector3 _currentVelocity;

    private ElementType _elementType;

    public void Init(ElementType elementType)
    {
        _elementType = elementType;
        _totalMass = elementType.BaseMass;
    }

    public void SetPosition(Vector3 aimPoint)
    {
        _centerPosition = aimPoint;
        if (_lastPosition == default)
        {
            _lastPosition = _centerPosition;
        }
        _currentVelocity = Vector3.Lerp(_currentVelocity, _centerPosition - _lastPosition, 0.1f);
        _lastPosition = _centerPosition;
    }

    public void Grow(float multiplier)
    {
        _totalMass += Time.deltaTime * multiplier;
        UpdateMass();
    }

    private void UpdateMass()
    {
        for (int i = 0; i < subMasses.Count; i++) {
            subMasses[i].SetMass(_totalMass);
        }
    }

    public void Fizzle()
    {
        //JAM add cool effects n stuff
        Destroy(gameObject);
    }
}
