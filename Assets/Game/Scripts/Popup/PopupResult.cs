using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupResult : Popup
{
    [SerializeField] private Text resultText;

    public override void Open()
    {
        SoundMgr.Instance.Play(SoundType.RESULT);

        Time.timeScale = 0;

        resultText.text = string.Empty;
        resultText.text += string.Format("시간 : {0}\n", GameMgr.Instance.TimeValue);
        resultText.text += string.Format("레벨 : {0}\n", Player.CurrentPlayer.Level);
        resultText.text += string.Format("처치한 적 : {0}\n", Player.CurrentPlayer.MonsterDeathCount);
    }

    public override void Close()
    {
        Time.timeScale = 1;
    }

    public void OnClickExit()
    {
        Time.timeScale = 1;

        SoundMgr.Instance.Play(SoundType.BUTTON);
        GameMgr.Instance.OnClickBack();
    }
}
