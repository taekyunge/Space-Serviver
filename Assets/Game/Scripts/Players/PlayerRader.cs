using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 레이더 (자기장 영역)
/// </summary>
public class PlayerRader : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    [SerializeField] private Transform raderTransform;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetRadius(float value)
    {
        circleCollider.radius = value;

        raderTransform.transform.localScale = new Vector3(value, value, 0);
    }
}
