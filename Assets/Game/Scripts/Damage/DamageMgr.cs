using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMgr : Singleton<DamageMgr>
{
    [SerializeField]
    private DamageText baseDamage;

    private Pooling<DamageText> damagePool;

    private void Start()
    {
        damagePool = new Pooling<DamageText>(10, baseDamage, transform);
    }

    public void SetDamage(Transform target, float damage)
    {
        var damageFont = damagePool.Get();

        damageFont.transform.position = target.position;
        damageFont.Play(damage, Color.red);
    }

    public void SetDamage(Transform target, float damage, Color color)
    {
        var damageFont = damagePool.Get();

        damageFont.transform.position = target.position;
        damageFont.Play(damage, color);
    }

    public void DeleteDamage(DamageText damageFont)
    {
        damagePool.Delete(damageFont);
    }
}
