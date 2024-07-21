using System.Collections.Generic;
using UnityEngine;

public class CursorUnlocker : MonoBehaviour
{
    public static List<CursorUnlocker> ReadOnlyInstancesList = new List<CursorUnlocker>();

    private void OnEnable()
    {
        ReadOnlyInstancesList.Add(this);

    }
    private void OnDisable()
    {
        ReadOnlyInstancesList.Remove(this);
    }
}
