using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이펙트
/// </summary>
public class Effect : MonoBehaviour
{
    public EffectType effectType;

    [SerializeField] private ParticleSystem particleSystem;

    public void Play()
    {
        particleSystem.Play();
    }

    private void Update()
    {
        if (!particleSystem.isPlaying)
            EffectMgr.Instance.Delete(this);
    }
}
