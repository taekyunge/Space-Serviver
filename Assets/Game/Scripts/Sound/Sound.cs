using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 사운드
/// </summary>
public class Sound : MonoBehaviour
{
    public SoundType SoundType;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
            SoundMgr.Instance.DeleteSound(this);
    }
}
