using UnityEngine;
using System.Collections;

/// <summary>
/// 인게임 Bounds
/// </summary>
public class GameBounds : MonoBehaviour
{
    public bool UpdateBounds = false;
    public Vector2 Min;
    public Vector2 Max;
    public Vector2 Size;

    [HideInInspector] public float Width;
    [HideInInspector] public float Height;
}
