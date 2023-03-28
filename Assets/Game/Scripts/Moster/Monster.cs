using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    private MoveController moveController;
    private Transform target;
    private Collider2D collider;

    [SerializeField] private GaugeBar gaugeBar;
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private Animator animator;

    public float Hp = 100;
    public float MoveSpeed = 5;
    public float Damage = 1;

    private float hitDelay = 0;
    private float pushDelay = 0;
    private Vector2 pushDir;

    [SerializeField] private AnimationCurve hpCurve;
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private AnimationCurve damageCureve;

    private void Awake()
    {
        moveController = GetComponent<MoveController>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        gaugeBar.value = Mathf.Lerp(gaugeBar.value, Hp, Time.deltaTime * 10);

        if(hitDelay > 0)
            hitDelay -= Time.deltaTime;
        else
            hitDelay = 0;

        if (pushDelay > 0)
            pushDelay -= Time.deltaTime;
        else
            pushDelay = 0;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        var distance = target.position - transform.position;
        var angleAxis = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        if(pushDelay > 0)
           moveController.Move(pushDir.normalized * MoveSpeed * 1.5f * Time.fixedDeltaTime);
        else
            moveController.Move(distance.normalized * MoveSpeed * Time.fixedDeltaTime);

        var eulerAngles = Quaternion.AngleAxis((angleAxis > 90 || angleAxis < -90) ? 180 - angleAxis : angleAxis, Vector3.forward).eulerAngles;

        eulerAngles.y = (angleAxis > 90 || angleAxis < -90) ? 180 : 0;

        rotateTransform.rotation = Quaternion.Euler(eulerAngles);
    }

    public void Initialize()
    {
        Hp = hpCurve.Evaluate(Player.CurrentPlayer.Level);
        MoveSpeed = speedCurve.Evaluate(Player.CurrentPlayer.Level);
        Damage = damageCureve.Evaluate(Player.CurrentPlayer.Level);

        hitDelay = 0;
        target = Player.CurrentPlayer.transform;
        collider.enabled = true;
    }

    public bool Hit(Vector2 force, float damage)
    {
        SoundMgr.Instance.Play(SoundType.HIT);

        Hp -= damage;

        EffectMgr.Instance.Play(EffectType.BLOOD, transform.position, rotateTransform.rotation);
        DamageMgr.Instance.SetDamage(gaugeBar.transform, damage);

        if (Hp <= 0)
        {
            Delete();

            return true;
        }

        pushDelay = 0.25f;
        pushDir = force;

        return false;
    }

    public void Delete()
    {
        SoundMgr.Instance.Play(SoundType.MONSTER_DEATH);

        Hp = 0;

        target = null;
        collider.enabled = false;
        animator.SetTrigger("Death");        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (hitDelay <= 0)
            {
                var player = collision.gameObject.GetComponent<Player>();

                if (player != null)
                    player.Hit(Damage);

                hitDelay = 0.5f;
            }
        }
    }
}
