using UnityEngine;

public static class Util
{
    /// <summary>
    /// ty freya holmer https://www.youtube.com/watch?v=LSNQuFEDOyQ
    /// </summary>
    public static float ExpDecayLerp(float a, float b, float decay, float deltaTime)
    {
        return b + (a - b) * Mathf.Exp(-decay * deltaTime);
    }
    /// <summary>
    /// ty freya holmer https://www.youtube.com/watch?v=LSNQuFEDOyQ
    /// </summary>
    public static Vector3 ExpDecayLerp(Vector3 a, Vector3 b, float decay, float deltaTime)
    {
        return b + (a - b) * Mathf.Exp(-decay * deltaTime);
    }
}
