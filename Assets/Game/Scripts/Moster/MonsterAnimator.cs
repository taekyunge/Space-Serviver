using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터 애니메이터
/// </summary>
public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] private Monster monster;

    public void Death()
    {
        ItemMgr.Instance.CreateItem(transform.position, ItemType.COIN);

        MonsterMgr.Instance.DeleteMonster(monster);
    }
}
