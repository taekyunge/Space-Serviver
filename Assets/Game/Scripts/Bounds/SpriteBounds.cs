using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBounds : GameBounds
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            return;

        Size = spriteRenderer.size;

        Width = spriteRenderer.size.x;
        Height = spriteRenderer.size.y;

        SetBounds();
    }

    private void Update()
    {
        if (!UpdateBounds || spriteRenderer == null)
            return;

        SetBounds();
    }

    private void SetBounds()
    {
        Min.x = spriteRenderer.bounds.min.x;
        Min.y = spriteRenderer.bounds.min.y;
        Max.x = spriteRenderer.bounds.max.x;
        Max.y = spriteRenderer.bounds.max.y;
    }
}
