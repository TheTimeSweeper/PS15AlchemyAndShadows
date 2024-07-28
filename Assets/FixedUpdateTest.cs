using SpellCasting;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedUpdateTest : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool goByFixed;

    private Vector3 CurrentPOsition;
    private Vector3 _lastFixedUpdatePosition;

    private List<Vector3> fixedPositons = new List<Vector3>();
    private List<Vector3> updatePositons = new List<Vector3>();

    private float _fixedTime;

    void Update()
    {
        if (!goByFixed)
        {
            CurrentPOsition += Vector3.right * speed * Time.deltaTime;
        }

        float timeSinceLastDeltaTIme = Time.time - _fixedTime;

        //good
        Vector3 lerpedPosition = Vector3.Lerp(_lastFixedUpdatePosition, CurrentPOsition, timeSinceLastDeltaTIme / Time.fixedDeltaTime);

        //bad
        //Vector3 direction = CurrentPOsition - _lastFixedUpdatePosition;

        //Vector3 lerpedPosition = _lastFixedUpdatePosition + direction * timeSinceLastDeltaTIme;

        updatePositons.Add(lerpedPosition);

        Debug();
    }

    void FixedUpdate()
    {
        _lastFixedUpdatePosition = CurrentPOsition;
        if (goByFixed)
        {
            CurrentPOsition += Vector3.right * speed * Time.fixedDeltaTime;
            speed += 0.1f;
        }
        fixedPositons.Add(CurrentPOsition);
        _fixedTime = Time.time;
    }

    private void Debug()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            fixedPositons.Clear();
            updatePositons.Clear();
            CurrentPOsition = transform.position;
        }

        var offBlue = Color.blue;
        offBlue.a = 0.5f;

        for (int i = 0; i < fixedPositons.Count; i++)
        {
            UnityEngine.Debug.DrawLine(fixedPositons[i], fixedPositons[i] + Vector3.up * 1, Color.white );
        }

        for (int i = 0; i < updatePositons.Count; i++)
        {
            UnityEngine.Debug.DrawLine(updatePositons[i] + Vector3.up * 2, updatePositons[i] + Vector3.up * 3, offBlue);
        }
    }
}
