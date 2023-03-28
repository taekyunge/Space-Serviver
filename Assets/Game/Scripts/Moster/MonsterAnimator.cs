using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] private Monster monster;

    public void Death()
    {
        ItemMgr.Instance.CreateItem(transform.position, ItemType.COIN);

        MonsterMgr.Instance.DeleteMonster(monster);
    }
}
