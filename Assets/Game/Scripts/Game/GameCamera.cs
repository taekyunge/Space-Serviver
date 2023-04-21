using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임 카메라
/// </summary>
public class GameCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    [SerializeField] private SpriteBounds mapBounds;

    private CameraBounds cameraBounds;

    private void Start()
    {
        cameraBounds = GetComponent<CameraBounds>();

        if (Player.CurrentPlayer != null)
            transform.position = Player.CurrentPlayer.transform.position + offset;
    }

    private void FixedUpdate()
    {
        if (Player.CurrentPlayer == null || cameraBounds == null)
            return;

        Vector3 position = Player.CurrentPlayer.transform.position + offset;

        // 카메라 화면이 맵 범위를 벗어나지 않도록 고정
        position.x = Mathf.Clamp(position.x, mapBounds.Min.x + cameraBounds.Width, mapBounds.Max.x - cameraBounds.Width);
        position.y = Mathf.Clamp(position.y, mapBounds.Min.y + cameraBounds.Height, mapBounds.Max.y - cameraBounds.Height);

        transform.position = Vector3.Lerp(transform.position, position, speed * Time.fixedDeltaTime);
    }
}
