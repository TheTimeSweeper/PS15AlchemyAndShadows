using SpellCasting;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// lerp but framerate independent. ty freya holmer https://www.youtube.com/watch?v=LSNQuFEDOyQ
    /// </summary>
    public static float ExpDecayLerp(float a, float b, float decay, float deltaTime)
    {
        return b + (a - b) * Mathf.Exp(-decay * deltaTime);
    }
    /// <summary>
    /// lerp but framerate independent. ty freya holmer https://www.youtube.com/watch?v=LSNQuFEDOyQ
    /// </summary>
    public static Vector3 ExpDecayLerp(Vector3 a, Vector3 b, float decay, float deltaTime)
    {
        return b + (a - b) * Mathf.Exp(-decay * deltaTime);
    }

    public static TeamIndex GetTeamIndex(IHasCommonComponents component)
    {
        return component.CommonComponents.TeamComponent.TeamIndex;
    }

    public static bool HasFlag(this uint flag1, uint flag2){
        return (flag1 & flag2) == flag2;
    }
}
