using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    private int selectPlayer = 0;

    public void OnClickCharacter(int number)
    {
        SoundMgr.Instance.Play(SoundType.BUTTON);
        selectPlayer = number;
    }

    public void OnClickStart()
    {
        SoundMgr.Instance.Play(SoundType.BUTTON);

        GameMgr.SelectPlayer = selectPlayer;

        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        SoundMgr.Instance.Play(SoundType.BUTTON);

        Application.Quit();
    }
}
