using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "GestureType/Shake", fileName = "GestureShake")]
public class ShakeGestureHorizontal : AimGesture
{
    [SerializeField]
    private float maxYGive = 3;

    [SerializeField]
    private int minShakes = 3;

    [SerializeField]
    private float minXTurn = 3;

    private Vector3 _lastDelta;
    private Vector3 _lastExtreme;

    private float _graceTimer = 0.5f;

    private int _turns;

    private bool _lastQualified;

    public override void ResetGesture()
    {
        _lastDelta = default;
        _lastExtreme = default;
        _turns = 0;
    }

    public override bool QualifyGesture(InputBank bank)
    {
        _graceTimer -= Time.deltaTime;

        if (bank.GestureDelta == Vector3.zero)
        {
            if (_graceTimer < 0)
            {
                _turns = 0;
                bank.ShakeTurns = _turns;
            }
            else
            {
                return _lastQualified;
            }
        }else
        {
            _graceTimer = 0.5f;
        }

        int direction = bank.GestureDelta.x > 0 ? 1 : -1;

        //is previous direction opposite current direction
        if(_lastDelta.x * -direction > 0)
        {
            if (Mathf.Abs(bank.GesturePosition.x - _lastExtreme.x) > minXTurn)
            {
                _turns++;
            }
            _lastExtreme = bank.GesturePosition;
        }

        _lastDelta = bank.GestureDelta;

        if (Mathf.Abs(bank.GesturePosition.y - _lastExtreme.y) > maxYGive)
        {
            _turns = 0;
        }

        bank.ShakeTurns = _turns;

        _lastQualified = _turns >= minShakes;

        return _lastQualified;
    }
}
