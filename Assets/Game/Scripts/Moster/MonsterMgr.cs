using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMgr : Singleton<MonsterMgr>
{
    private Pooling<Monster> monsterPool = null;

    [SerializeField] private Text countText;
    [SerializeField] private CameraBounds cameraBounds;
    [SerializeField] private SpriteBounds mapBounds;
    [SerializeField] private Monster baseMonster = null;
    [SerializeField] private float createTime = 10f;
    [SerializeField] private AnimationCurve createCountCurve;
    [SerializeField] private AnimationCurve createTimeCurve;

    private float deltaTime = 0f;

    private void Start()
    {
        monsterPool = new Pooling<Monster>(1, baseMonster, transform);

        CreateMonsters();
    }

    private void Update()
    {
        countText.text = string.Format("{0} 마리", monsterPool.count);

        if (deltaTime > createTime)
        {
            deltaTime = 0;

            CreateMonsters();
        }

        deltaTime += Time.deltaTime;
    }

    private void CreateMonsters()
    {
        createTime = createTimeCurve.Evaluate(Player.CurrentPlayer.Level);

        var createCount = Mathf.RoundToInt(createCountCurve.Evaluate(Player.CurrentPlayer.Level));

        for (int i = 0; i < createCount; i++)
        {
            var monster = monsterPool.Get(GetRandomPosition());

            monster.Initialize();
            monster.transform.localScale = Vector3.one;
        }
    }

    public void DeleteMonsterAll()
    {
        var monsters = monsterPool.GetAll();

        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].Delete();
        }
    }

    public void DeleteMonster(Monster monster)
    {
        monsterPool.Delete(monster);
    }

    public Vector3 GetRandomPosition()
    {
        return Utill.GetRandomPointBetweenBounds(cameraBounds, mapBounds);
    }
}
