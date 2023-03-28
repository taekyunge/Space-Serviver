using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private MoveController moveController;

    private Vector2 movement;
    private float deltaTime;
    private int deathCount;

    private void Start()
    {
        moveController = GetComponent<MoveController>();
    }

    private void Update()
    {
        if (Player.CurrentPlayer == null)
            return;

        deltaTime += Time.deltaTime;

        if(deltaTime > Player.CurrentPlayer.WeaponLifeTime)
        {
            deltaTime = 0;

            Player.CurrentPlayer.DelteWeapon(this);
        }
    }

    private void FixedUpdate()
    {
        if (Player.CurrentPlayer == null)
            return;

        moveController.Move(movement * Player.CurrentPlayer.WeaponMoveSpeed * Time.fixedDeltaTime);
    }

    public void Initialize()
    {
        if (Player.CurrentPlayer == null)
            return;

        deltaTime = 0;
        deathCount = Player.CurrentPlayer.WeaponDeathCount;
    }

    public void SetMovement(Vector2 movement)
    {
        this.movement = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Monster")
        {
            var monster = collision.gameObject.GetComponent<Monster>();

            if (monster != null)
            {
                Vector3 force = collision.transform.position - transform.position;

                var death = monster.Hit(force, Player.CurrentPlayer.WeaponDamage);

                deathCount--;

                if (death)
                    Player.CurrentPlayer.MonsterDeathCount++;

                if (deathCount <= 0)
                    Player.CurrentPlayer.DelteWeapon(this);
            }
        }
    }
}