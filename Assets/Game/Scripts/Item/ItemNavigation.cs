using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemNavigation : MonoBehaviour
{
    [HideInInspector] public Item TargetItem;

    [SerializeField] private Transform rotateTransform;
    [SerializeField] private Transform moveTransform;
    [SerializeField] private Image itemImage;
    [SerializeField] private CameraBounds cameraBounds;

    [SerializeField] private float speed = 10;

    private float offset = 2;

    public void SetItem(Item item)
    {
        TargetItem = item;

        if (item != null)
        {
            itemImage.sprite = SpriteMgr.Instance.GetSprite(Data.ItemNames[(int)TargetItem.Type]);
            itemImage.SetNativeSize();

            var distance = TargetItem.transform.position - Player.CurrentPlayer.transform.position;
            var angleAxis = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

            moveTransform.position = GetScreenPosition(angleAxis);
        }        
    }

    private void Update()
    {
        if (TargetItem != null)
        {
            var itemScreenPos = Camera.main.WorldToScreenPoint(TargetItem.transform.position);

            if (itemScreenPos.x >= 0 && itemScreenPos.x < Screen.width && itemScreenPos.y >= 0 && itemScreenPos.y < Screen.height)
            {
                moveTransform.gameObject.SetActive(false);
            }
            else
            {
                moveTransform.gameObject.SetActive(true);

                var distance = TargetItem.transform.position - Player.CurrentPlayer.transform.position;
                var angleAxis = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

                rotateTransform.localRotation = Quaternion.AngleAxis(angleAxis, Vector3.forward);
                moveTransform.position = Vector3.Lerp(transform.position, GetScreenPosition(angleAxis), Time.fixedDeltaTime * speed);
            }
        }
        else
            moveTransform.gameObject.SetActive(false);
    }

    private Vector3 GetScreenPosition(float angleAxis)
    {
        float x = 0;
        float y = 0;
             
        if (angleAxis > -135 && angleAxis < -45f)
        {
            x = GetPos(angleAxis, -135f, -45f, cameraBounds.Min.x + offset, cameraBounds.Max.x - offset);
            y = cameraBounds.Min.y + offset;
        }
        else if (angleAxis > 45 && angleAxis < 135)
        {
            x = GetPos(angleAxis, 135, 45, cameraBounds.Min.x + offset, cameraBounds.Max.x - offset);
            y = cameraBounds.Max.y - offset;
        }
        else if (angleAxis > -45 && angleAxis < 45)
        {
            x = cameraBounds.Max.x - offset;
            y = GetPos(angleAxis, -45, 45, cameraBounds.Min.y + offset, cameraBounds.Max.y - offset);
        }
        else
        {
            x = cameraBounds.Min.x + offset;

            if (angleAxis < -135)
                y = GetPos(angleAxis, -135, -180, cameraBounds.Min.y + offset, cameraBounds.Min.y + (cameraBounds.Size.y / 2));
            else
                y = GetPos(angleAxis, 180, 135, cameraBounds.Min.y + (cameraBounds.Size.y / 2), cameraBounds.Max.y - offset);
        }

        return Camera.main.WorldToScreenPoint(new Vector2(x, y));
    }

    private float GetPos(float angleAxis, float min1, float max1, float min2, float max2)
    {
        angleAxis = (angleAxis - min1) / (max1 - min1);

        return angleAxis * (max2 - min2) + min2;
    }
}
