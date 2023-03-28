using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        position.x = Mathf.Clamp(position.x, mapBounds.Min.x + cameraBounds.Width, mapBounds.Max.x - cameraBounds.Width);
        position.y = Mathf.Clamp(position.y, mapBounds.Min.y + cameraBounds.Height, mapBounds.Max.y - cameraBounds.Height);

        transform.position = Vector3.Lerp(transform.position, position, speed * Time.fixedDeltaTime);
    }
}
