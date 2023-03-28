using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
