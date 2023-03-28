using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : Singleton<GameMgr>
{
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
