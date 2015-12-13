using UnityEngine;

public static class Utility
{
    public static T ChooseOne<T>(T[] arr)
    {
        return arr[Random.Range(0, arr.Length - 1)];
    }

    public static float AroundValue(float value, float range)
    {
        return value + Random.Range(-range, range);
    }
}
