using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 데미지 폰트
/// </summary>
public class DamageText : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Text text;

    private void Start()
    {
        animator.StopPlayback();
    }

    public void Play(float damage, Color color)
    {
        text.text = string.Format("{0:#,0}", damage);
        text.color = color;

        animator.Play(0);
    }

    private void Complete()
    {
        DamageMgr.Instance.DeleteDamage(this);
    }
}
