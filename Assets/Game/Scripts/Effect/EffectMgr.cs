using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMgr : Singleton<EffectMgr>
{
    [SerializeField] private Effect[] effects;

    private Pooling<Effect>[] effectPools;

    private void Start()
    {
        effectPools = new Pooling<Effect>[effects.Length];

        for (int i = 0; i < effects.Length; i++)
        {
            effectPools[i] = new Pooling<Effect>(5, effects[i], transform);
        }
    }

    public Effect Play(EffectType effectType, Vector3 position)
    {
        var effect = effectPools[(int)effectType].Get();

        effect.transform.position = position;
        effect.Play();

        return effect;
    }
    public Effect Play(EffectType effectType, Vector3 position, Transform parent)
    {
        var effect = effectPools[(int)effectType].Get();

        effect.transform.SetParent(parent);
        effect.transform.position = position;
        effect.transform.localScale = Vector3.one;
        effect.Play();

        return effect;
    }

    public Effect Play(EffectType effectType, Vector3 position, Quaternion rotation)
    {
        var effect = effectPools[(int)effectType].Get();

        effect.transform.position = position;
        effect.transform.rotation = rotation;
        effect.Play();

        return effect;
    }

    public void Delete(Effect effect)
    {
        effectPools[(int)effect.effectType].Delete(effect);
    }
}
