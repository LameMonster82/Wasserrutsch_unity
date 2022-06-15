using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

namespace PASCALGERBERSCRIPT
{
public static class ExtensionMethods
{
    public static Tween TweenFloat(this float from, float to, float duration, Action<float> callback)
    {
        return DOTween.To(() => from, x => from = x, to, duration)
        .OnUpdate(() => {
            callback(from);
        });
    }

    // remaps float from one range to another
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;

    }

    public static bool HasLayer(this LayerMask layermask, int layer)
    {
        return layermask == (layermask | (1 << layer));
    }

    // forces list to a fixed length
    public static void ResizeList<T>(this List<T> list, int newCount)
    {
        if (newCount <= 0)
        {
            list.Clear();
        }
        else
        {
            while (list.Count > newCount) list.RemoveAt(list.Count - 1);
            while (list.Count < newCount) list.Add(default(T));
        }
    }

    // make sure that a list doesnt exceed a specific range
    public static void CutList<T>(this List<T> list, int maxCount)
    {
        while (list.Count > maxCount) list.RemoveAt(list.Count - 1);
    }

    // sets anchor point of a rect transform like the presets in the inspector do
    public static void SetPivot(this RectTransform rect, RectAnchorOrigin origin)
    {
        RectAnchorPreset preset = new RectAnchorPreset(origin);
        rect.anchorMin = preset.anchorMin;
        rect.anchorMax = preset.anchorMax;
        rect.pivot = preset.pivot;
    }

    public static void ClearChildren(this Transform parent)
    {
        foreach (Transform child in parent)
        {
            MonoBehaviour.Destroy(child.gameObject);
        }
    }

    // creates a simple gradient with color and alpha at 0% and 100%
    public static Gradient GetSimpleGradient(Color color1, Color color2, float alpha1, float alpha2)
    {
        Gradient gradient = new Gradient();

        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = color1;
        colorKeys[0].time = 0f;
        colorKeys[1].color = color2;
        colorKeys[0].time = 1f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = alpha1;
        alphaKeys[0].time = 0f;
        alphaKeys[1].alpha = alpha2;
        alphaKeys[0].time = 1f;

        return gradient;
    }
    
    // search for children with specific tag
    public static GameObject[] FindChildrenWithTag(this Transform parent, string tag)
    {
        List<GameObject> childsWithTag = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.tag == tag)
            {
                childsWithTag.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                childsWithTag.AddRange(FindChildrenWithTag(child, tag));
            }
        }

        return childsWithTag.ToArray();
    }

    // get rect for splitscreens from number of maximum screens and screen number
    public static Rect GetSplitScreenRect(int splitScreenCount, int splitScreenIndex)
    {
        splitScreenCount = Mathf.Clamp(splitScreenCount, 0, 4);
        splitScreenIndex = Mathf.Clamp(splitScreenIndex, 0, 3);

        CameraSplitScreenRects rects = new CameraSplitScreenRects();
        Rect rect = rects.cameraRects[splitScreenCount, splitScreenIndex];
        if (rect == Rect.zero) Debug.LogError("Split Screen Rect Error - Rect not defined");

        return rect;
    }

    public static CardinalDirection NextDirection(this CardinalDirection direction, bool clockwise)
    {
        int index = (int)direction;
        index += clockwise ? 1 : -1;

        if (index > 3) index = 0;
        if (index < 0) index = 3;

        return (CardinalDirection)index;
    }

    public static CardinalDirection OppositeDirection(this CardinalDirection direction)
    {
        int index = (int)direction;
        index += 2;
        if (index > 3) index -= 4;

        return (CardinalDirection)index;
    }

    public static Vector3 CardinalVector(this CardinalDirection direction)
    {
        switch (direction)
        {
            case CardinalDirection.North: return Vector3.forward;
            case CardinalDirection.East: return Vector3.right;
            case CardinalDirection.South: return Vector3.back;
            case CardinalDirection.West: return Vector3.left;
            default: return Vector3.zero;
        }
    }

    public static Quaternion CardinalRotation(this CardinalDirection direction)
    {
        Vector3 vector = direction.CardinalVector();
        return Quaternion.LookRotation(vector);
    }

    public static void DestroySingleton<T>(this GameObject obj, T singleton)
    {
        Debug.LogError($"Only 1 Instance of {typeof(T).Name} allowed in a scene, object ({obj.name}) was destroyed.");
        UnityEngine.Object.Destroy(obj);
    }
}

// presets for rect transform anchors and pivots
[System.Serializable]
public class RectAnchorPreset
{
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 pivot;

    float[] anchorMinX = new float[] { 0f, 0.5f, 1f, 0f, 0.5f, 1f, 0f, 0.5f, 1f };
    float[] anchorMinY = new float[] { 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0f, 0f, 0f };
    float[] anchorMaxX = new float[] { 0f, 0.5f, 1f, 0f, 0.5f, 1f, 0f, 0.5f, 1f };
    float[] anchorMaxY = new float[] { 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0f, 0f, 0f };
    float[] pivotX = new float[] { 0f, 0.5f, 1f, 0f, 0.5f, 1f, 0f, 0.5f, 1f };
    float[] pivotY = new float[] { 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 0f, 0f, 0f };

    public RectAnchorPreset(RectAnchorOrigin origin)
    {
        int i = (int)origin;

        anchorMin = new Vector2(anchorMinX[i], anchorMinY[i]);
        anchorMax = new Vector2(anchorMaxX[i], anchorMaxY[i]);
        pivot = new Vector2(pivotX[i], pivotY[i]);
    }
}

public enum RectAnchorOrigin
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

// rects for split screen from 0 to 4 screens
[System.Serializable]
public class CameraSplitScreenRects
{
    public Rect[,] cameraRects =
    {
        { Rect.zero, Rect.zero, Rect.zero, Rect.zero},
        { new Rect(0f, 0f, 1f, 1f), Rect.zero, Rect.zero, Rect.zero},
        { new Rect(0f, 0f, 0.5f, 1f), new Rect(0.5f, 0f, 0.5f, 1f), new Rect(), new Rect() },
        { new Rect(0f, 0.5f, 0.5f, 0.5f), new Rect(0.5f, 0.5f, 0.5f, 0.5f), new Rect(0f, 0f, 0.5f, 0.5f), new Rect(0.5f, 0f, 0.5f, 0.5f) },
        { new Rect(0f, 0.5f, 0.5f, 0.5f), new Rect(0.5f, 0.5f, 0.5f, 0.5f), new Rect(0f, 0f, 0.5f, 0.5f), new Rect(0.5f, 0f, 0.5f, 0.5f) }
    };
}

// for saving date time in json
[Serializable]
public struct JsonDateTime
{
    public long value;
    public static implicit operator DateTime(JsonDateTime jdt)
    {
        return DateTime.FromFileTimeUtc(jdt.value);
    }
    public static implicit operator JsonDateTime(DateTime dt)
    {
        JsonDateTime jdt = new JsonDateTime();
        jdt.value = dt.ToFileTimeUtc();
        return jdt;
    }

    /*
    var time = DateTime.Now;
    print(time);
    var json = JsonUtility.ToJson((JsonDateTime) time);
    print(json);
    DateTime timeFromJson = JsonUtility.FromJson<JsonDateTime>(json);
    print(timeFromJson);
    */
}

[System.Serializable]
public class MinMax
{
    public float min;
    public float max;

    public float Random { get { return UnityEngine.Random.Range(min, max); } }

    public MinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public virtual float Evaluate(float t)
    {
        return Mathf.Lerp(min, max, t);
    }

    public float InverseLerp(float t)
    {
        return Mathf.InverseLerp(min, max, t);
    }
}

[System.Serializable]
public class CurveMinMax : MinMax
{
    public AnimationCurve curve;

    public CurveMinMax(float min, float max, AnimationCurve curve) : base(min, max)
    {
        this.min = min;
        this.max = max;
        this.curve = curve;
    }

    public override float Evaluate(float t)
    {
        float curveValue = curve.Evaluate(t);
        return Mathf.Lerp(min, max, curveValue);
    }
}

public enum CardinalDirection
{
    North,
    East,
    South,
    West
}

[System.Serializable]
public class CurveVFX
{
    public string floatName;
    public CurveMinMax curve;
    public VisualEffect vfx;

    public void Apply(float t)
    {
        vfx.SetFloat(floatName, curve.Evaluate(t));
    }
}

[System.Serializable]
public struct InOut<T>
{
    public T InValue;
    public T OutValue;

    public T GetValue(bool inValue)
    {
        return inValue ? InValue : OutValue;
    }
}
}
