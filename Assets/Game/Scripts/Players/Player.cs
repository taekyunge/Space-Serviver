using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 플레이어
/// </summary>
public class Player : MonoBehaviour
{
    public static Player CurrentPlayer;
    private Animator animator;
    private MoveController moveController;
    private CapsuleCollider2D collider;

    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private Pooling<Weapon> weaponPool;
    [SerializeField] private Weapon baseWeapon;
    [SerializeField] private GaugeBar gaugeBar;
    [SerializeField] private SpriteBounds mapBounds;
    [SerializeField] private PlayerRader playerRader;
    [SerializeField] private Text[] Texts;

    public float Hp = 100;
    public int Level = 1;
    public int Exp = 0;
    public int TotalExp = 20;
    public int Coin = 0;
    public float MoveSpeed = 5;
    public float WeaponSpeed = 1;
    public int WeaponCount = 1;
    public float WeaponAngle = 1;
    public float WeaponMoveSpeed = 0;
    public float WeaponLifeTime = 0;
    public float WeaponDamage = 0;
    public int WeaponDeathCount = 0;
    public float RaderRadius = 0.7f;

    public int MonsterDeathCount = 0;

    private float angleAxis;
    private Vector2 colliderSize;
    private bool invinciblility = false;
    private float invinciblilityTime = 0;

    private void Awake()
    {
        moveController = GetComponent<MoveController>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();

        colliderSize = collider.size;
    }

    private void Start()
    {
        weaponPool = new Pooling<Weapon>(20, baseWeapon, transform);
    }

    private void Update()
    {
        gaugeBar.value = Mathf.Lerp(gaugeBar.value, Hp, Time.deltaTime * 10);

        Texts[0].text = string.Format("체력 : {0} / 100", Hp);
        Texts[1].text = string.Format("데미지 : {0}", WeaponDamage);
        Texts[2].text = string.Format("속도 : {0}", MoveSpeed);
        Texts[3].text = string.Format("관통력 : {0}", WeaponDeathCount);
        Texts[4].text = string.Format("투사체 수  : {0}", WeaponCount);
        Texts[5].text = string.Format("공격속도 : {0}", WeaponSpeed);
        Texts[6].text = (Level >= Data.MaxLevel) ? string.Format("레벨 : MAX") : string.Format("레벨 : {0} ({1}%)", Level, Exp * 100 / TotalExp);
        Texts[7].text = string.Format("자기장 범위 : {0}", RaderRadius);

        // 무적 시간 체크
        if (invinciblilityTime <= 0)
        {
            invinciblilityTime = 0;
            invinciblility = false;
        }

        invinciblilityTime -= Time.deltaTime;

        playerRader.SetRadius(RaderRadius);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void LateUpdate()
    {
        MoveLimit();
    }

    private void Move()
    {
        Vector2 movement = new Vector2(UltimateJoystick.GetHorizontalAxis("Movement"), UltimateJoystick.GetVerticalAxis("Movement"));

        if (movement == Vector2.zero)
            return;

        moveController.Move(movement * MoveSpeed * Time.fixedDeltaTime);
    }

    private void MoveLimit()
    {
        // 플레이어가 맵 밖으로 나가지 않도록 고정
        var position = transform.position;

        position.x = Mathf.Clamp(position.x, mapBounds.Min.x + collider.size.x, mapBounds.Max.x - collider.size.x);
        position.y = Mathf.Clamp(position.y, mapBounds.Min.y + collider.size.y, mapBounds.Max.y - collider.size.y);

        transform.position = position;
    }

    private void Rotate()
    {
        Vector2 movement = new Vector2(UltimateJoystick.GetHorizontalAxis("Rotate"), UltimateJoystick.GetVerticalAxis("Rotate"));

        if (movement == Vector2.zero)
            return;

        angleAxis = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        var eulerAngles = Quaternion.AngleAxis((angleAxis > 90 || angleAxis < -90) ? 180 - angleAxis : angleAxis, Vector3.forward).eulerAngles;

        eulerAngles.y = (angleAxis > 90 || angleAxis < -90) ? 180 : 0;

        rotateTransform.rotation = Quaternion.Euler(eulerAngles);
    }

    private void Attack()
    {
        SoundMgr.Instance.Play(SoundType.ATTACK);

        animator.speed = WeaponSpeed;

        var angleAxis = this.angleAxis - (((WeaponCount - 1) * WeaponAngle) / 2);

        for (int i = 0; i < WeaponCount; i++)
        {
            var weapon = weaponPool.Get();
            var rotation = Quaternion.AngleAxis(angleAxis + (i * WeaponAngle), Vector3.forward);

            weapon.Initialize();
            weapon.transform.SetParent(transform.parent);
            weapon.transform.SetAsFirstSibling();
            weapon.transform.position = weaponTransform.position;
            weapon.transform.rotation = rotation;
            weapon.SetMovement(rotation.normalized * Vector3.right);
        }
    }

    public void Hit(float damage)
    {
        if (invinciblility)
            return;

        SoundMgr.Instance.Play(SoundType.PLAYER_HIT);

        Hp -= damage;

        if (Hp <= 0)
        {
            PopupMgr.Instance.CloseAll();
            PopupMgr.Instance.Open(PopupType.RESULT);
        }
        else
            DamageMgr.Instance.SetDamage(gaugeBar.transform, damage);
    }

    private void LevelUp()
    {
        Level++;

        if(Level >= Data.MaxLevel)
        {
            PopupMgr.Instance.CloseAll();
            PopupMgr.Instance.Open(PopupType.RESULT);
        }
        else
        {
            Exp = 0;
            TotalExp = Data.AmountExp[Level - 1];
            PopupMgr.Instance.Open(PopupType.SELECT_ITEM);
        }
    }

    public void UseItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.HP:
                {
                    Hp = 100;

                    DamageMgr.Instance.SetDamage(gaugeBar.transform, 20, Color.green);
                }
                break;

            case ItemType.BOMB:
                {
                    MonsterMgr.Instance.DeleteMonsterAll();
                }
                break;

            case ItemType.COIN:
                {
                    Exp += 10;

                    if(Exp >= Data.AmountExp[Level - 1])
                    {
                        LevelUp();
                    }
                }
                break;

            case ItemType.MAGNET:
                {
                    RaderRadius = Mathf.Clamp(RaderRadius + 0.5f, 0, 8);
                }
                break;

            case ItemType.POWER_UP:
                {
                    WeaponDamage += 10;
                }
                break;

            case ItemType.SPEED_UP:
                {
                    MoveSpeed = Mathf.Clamp(MoveSpeed + 0.5f, 0, 10);
                }
                break;

            case ItemType.DEATH_COUNT_UP:
                {
                    WeaponDeathCount = Mathf.Clamp(WeaponDeathCount + 1, 0, 10);
                }
                break;

            case ItemType.COUNT_UP:
                {
                    WeaponCount = Mathf.Clamp(WeaponCount + 1, 0, 72);
                }
                break;

            case ItemType.COOLTIME_UP:
                {
                    WeaponSpeed = Mathf.Clamp(WeaponSpeed + 0.1f, 0, 3);
                }
                break;

            case ItemType.INVINCIBLILITY:
                {
                    invinciblility = true;
                    invinciblilityTime = 5;
                }
                break;
        }
    }

    public void DelteWeapon(Weapon weapon)
    {
        weaponPool.Delete(weapon);
    }
}
