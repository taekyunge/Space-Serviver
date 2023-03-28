using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMgr : Singleton<PopupMgr>
{
    [SerializeField] private Popup[] popups;

    public void Open(PopupType popupType)
    {
        int index = (int)popupType;

        popups[index].gameObject.SetActive(true);
        popups[index].Open();
    }

    public void Close(PopupType popupType)
    {
        int index = (int)popupType;

        popups[index].Close();
        popups[index].gameObject.SetActive(false);
    }

    public void CloseAll()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            popups[i].Close();
            popups[i].gameObject.SetActive(false);
        }
    }
}
