using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템
/// </summary>
public class Item : MonoBehaviour
{
    private MoveController moveController;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public ItemType Type;
    public float MoveSpeed = 5;
    private Transform target;
    private Effect dustEffect;

    private void Awake()
    {
        moveController = GetComponent<MoveController>();
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        // 플레이어 자기장 영역안에 들어왔을 경우 플레이어에게 빨려들어가도록 움직임
        var movement = target.position - transform.position;

        moveController.Move(movement.normalized * MoveSpeed * Time.fixedDeltaTime);
    }

    private void OnBecameVisible()
    {
        Debug.Log(gameObject);
    }

    public void Initialize()
    {
        target = null;

        dustEffect = EffectMgr.Instance.Play(EffectType.DUST, transform.position, transform);
    }

    public void SetItem(ItemType type)
    {
        int index = (int)type;

        Type = type;
        spriteRenderer.sprite = SpriteMgr.Instance.GetSprite(Data.ItemNames[index]);
    }

    private void UseItem()
    {
        SoundMgr.Instance.Play(SoundType.ITEM);

        Player.CurrentPlayer.UseItem(Type);

        ItemMgr.Instance.DeleteItem(this);

        EffectMgr.Instance.Play(EffectType.SPARK, transform.position);

        if (dustEffect != null)
        {
            EffectMgr.Instance.Delete(dustEffect);
            dustEffect = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Rader")
        {
            target = Player.CurrentPlayer.transform;
        }

        if (collision.transform.tag == "Player")
        {
            UseItem();
        }
    }
}
