using SpellCasting;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

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

    public static T WeightedRandom<T>(this List<T> items, Func<T, float> getWeight)
    {
        List<float> weightList = new List<float>();

        for (int i = 0; i < items.Count; i++)
        {
            weightList.Add(getWeight(items[i]));
        }

        Normalize(weightList);

        float sum = 0f;
        float rand = UnityEngine.Random.value;

        for (var i = 0; i < weightList.Count; i++)
        {
            if (weightList[i] == 0)
                continue;

            sum += weightList[i];

            if (rand <= sum)
            {
                return items[i];
            }
        }

        return default;
    }

    public static void Normalize(this List<float> weights)
    {
        float sum = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            sum += weights[i];
        }
        if (sum > 0)
        {
            for (int i = 0; i < weights.Count; i++)
            {
                weights[i] /= sum;
            }
        }
    }

    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {

            int rnd = UnityEngine.Random.Range(0, i);
            T temp = list[i];

            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}
