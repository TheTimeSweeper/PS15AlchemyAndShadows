using System;
using UnityEngine;

public class ElementSubMass : MonoBehaviour
{
    [SerializeField]
    private Transform viewContainer;

    [SerializeField]
    private Rigidbody rigidBody;

    public void MoveTowards(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, 0.1f);
    }

    public void SetMass(float totalMass)
    {
        viewContainer.transform.localScale = Vector3.one * totalMass;
    }

    public void TEMPThrow(Vector3 throwVector)
    {
        rigidBody.isKinematic = false;
        rigidBody.velocity = throwVector;
    }
}
