using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sprite 영역 계산
/// </summary>
public class SpriteBounds : GameBounds
{
    private SpriteRenderer spriteRenderer;

    public new Vector2 Min { get { return spriteRenderer.bounds.min; } }
    public new Vector2 Max { get { return spriteRenderer.bounds.max; } }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            return;

        Size = spriteRenderer.size;

        Width = spriteRenderer.size.x;
        Height = spriteRenderer.size.y;
    }
}
