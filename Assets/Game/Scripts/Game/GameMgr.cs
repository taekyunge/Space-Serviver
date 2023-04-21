using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 게임 매니져
/// </summary>
public class GameMgr : Singleton<GameMgr>
{
    /// <summary>
    /// 선택된 플레이어 인덱스
    /// </summary>
    public static int SelectPlayer = 0;
    public string TimeValue;

    [SerializeField] private Text timeText;
    [SerializeField] private Player[] players;

    private float timeValue;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.SetActive(i == SelectPlayer);

            if(i == SelectPlayer)
            {
                Player.CurrentPlayer = players[i];
            }
        }
    }

    private void Update()
    {
        // 플레이 타임 누적
        TimeValue = System.TimeSpan.FromSeconds(timeValue).ToString(@"mm\:ss");
        timeText.text = TimeValue;

        timeValue += Time.deltaTime;
    }

    public void OnClickBack()
    {
        SoundMgr.Instance.Play(SoundType.BUTTON);

        SceneManager.LoadScene(0);
    }
}
