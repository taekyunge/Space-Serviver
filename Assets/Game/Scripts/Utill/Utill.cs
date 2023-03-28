using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utill
{
    public static string GetMiddleString(string str, string begin, string end)
    {
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }

        string result = null;

        if (str.IndexOf(begin) > -1)
        {
            str = str.Substring(str.IndexOf(begin) + begin.Length);
            if (str.IndexOf(end) > -1) result = str.Substring(0, str.IndexOf(end));
            else result = str;
        }

        return result;
    }

    public static Color ToColor32(float r, float g, float b, float a)
    {
        return new Color(r / 255, g / 255, b / 255, a);
    }

    public static float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    public static Vector3 GetRandomPointBounds(GameBounds bounds)
    {
        return new Vector2(Random.Range(bounds.Min.x, bounds.Max.x), Random.Range(bounds.Min.y, bounds.Max.y));
    }

    public static Vector2 GetRandomPointBoundsBorder(Bounds bounds)
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                //up
                return new Vector2(Random.Range(bounds.min.x, bounds.max.x), bounds.max.y);
            case 1:
                //down
                return new Vector2(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y);
            case 2:
                //left
                return new Vector2(bounds.min.x, Random.Range(bounds.min.y, bounds.max.y));
            default:
                //rigth
                return new Vector2(bounds.max.x, Random.Range(bounds.min.y, bounds.max.y));
        }
    }

    public static Vector2 GetRandomPointBetweenBounds(GameBounds minBounds, GameBounds maxBounds)
    {
        float point = Random.Range(maxBounds.Min.x, maxBounds.Max.x);

        if (point > minBounds.Min.x && point < minBounds.Max.x)
        {
            //up & down
            if (Random.Range(0, 2) == 0)
            {
                return new Vector2(point, Random.Range(minBounds.Max.y, maxBounds.Max.y));
            }
            else
            {
                return new Vector2(point, Random.Range(maxBounds.Min.y, minBounds.Min.y));
            }
        }
        else
        {
            //left & right
            return new Vector2(point, Random.Range(maxBounds.Min.y, maxBounds.Max.y));
        }
    }

    public static Vector3 GetRandomPointCircleBorder(float radius)
    {
        return Random.insideUnitCircle.normalized * radius;
    }
}
