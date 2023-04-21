using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 선택 화면
/// </summary>
public class PopupSelectItem : Popup
{
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;
    [SerializeField] private Toggle[] toggles;

    private List<ItemType> randomItems = new List<ItemType>();
    private ItemType[] randomTypes = new ItemType[3];

    private int selectItem = 0;

    private void Initialize()
    {
        selectItem = 0;

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = (i == selectItem);
        }

        if(randomItems.Count == 0)
        {
            for (int i = 0; i < (int)ItemType.COIN; i++)
            {
                randomItems.Add((ItemType)i);
            }
        }
    }

    public override void Open()
    {
        SoundMgr.Instance.Play(SoundType.LEVEL_UP);

        Time.timeScale = 0;

        Initialize();

        randomItems = randomItems.OrderBy(a => System.Guid.NewGuid()).ToList();

        for (int i = 0; i < randomTypes.Length; i++)
        {
            randomTypes[i] = randomItems[i];
            images[i].sprite = SpriteMgr.Instance.GetSprite(Data.ItemNames[(int)randomItems[i]]);

            string textValue = string.Empty;

            switch (randomTypes[i])
            {
                case ItemType.HP:
                    textValue = "체력을 100% 회복 합니다";
                    break;
                case ItemType.BOMB:
                    textValue = "맵 전체의 몬스터들을 전부 제거 합니다";
                    break;
                case ItemType.MAGNET:
                    textValue = "캐릭터 주변의 자기장의 범위가 0.5 증가 합니다";
                    break;
                case ItemType.POWER_UP:
                    textValue = "데미지가 10 증가 합니다";
                    break;
                case ItemType.SPEED_UP:
                    textValue = "속도가 0.5 증가 합니다";
                    break;
                case ItemType.DEATH_COUNT_UP:
                    textValue = "관통력이 1 증가 합니다";
                    break;
                case ItemType.COUNT_UP:
                    textValue = "투사체수가 1 증가 합니다";
                    break;
                case ItemType.COOLTIME_UP:
                    textValue = "공격 속도가 0.1 증가 합니다";
                    break;
                case ItemType.INVINCIBLILITY:
                    textValue = "5초간 무적";
                    break;
            }

            texts[i].text = textValue;
        }
    }

    public override void Close()
    {
        Time.timeScale = 1;

        Player.CurrentPlayer.UseItem(randomTypes[selectItem]);

        Initialize();
    }

    public void OnSelectItem(int number)
    {
        selectItem = number;
    }

    public void OnClickEnter()
    {
        SoundMgr.Instance.Play(SoundType.BUTTON);
        PopupMgr.Instance.Close(PopupType.SELECT_ITEM);
    }
}
