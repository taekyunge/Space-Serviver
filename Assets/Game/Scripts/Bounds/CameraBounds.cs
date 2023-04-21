using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 화면 영역 계산
/// </summary>
public class CameraBounds : GameBounds
{
    private Camera boundsCamera;

    private void Awake()
    {
        boundsCamera = GetComponent<Camera>();

        if (boundsCamera == null)
            return;
        
        Width = boundsCamera.orthographicSize * boundsCamera.aspect;
        Height = boundsCamera.orthographicSize;

        Size.x = Width * 2;
        Size.y = Height * 2;

        SetBounds();
    }

    private void Update()
    {
        if (!UpdateBounds || boundsCamera == null)
            return;

        SetBounds();
    }

    private void SetBounds()
    {
        Min.x = boundsCamera.transform.position.x - Width;
        Min.y = boundsCamera.transform.position.y - Height;
        Max.x = boundsCamera.transform.position.x + Width;
        Max.y = boundsCamera.transform.position.y + Height;
    }
}
