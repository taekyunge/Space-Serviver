using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemMgr : Singleton<ItemMgr>
{
    private Pooling<Item> itemPool;

    [SerializeField] private Item baseItem;
    [SerializeField] private SpriteBounds mapBounds;
    [SerializeField] private float createTime;
    [SerializeField] private ItemNavigation[] itemNavigations;

    private float deltaTime = 0;

    private void Start()
    {
        itemPool = new Pooling<Item>(10, baseItem, transform);
    }

    private void Update()
    {
        if(deltaTime > createTime)
        {
            deltaTime = 0;

            CreateRandomItem();
        }

        deltaTime += Time.deltaTime;

        var useItems = itemPool.GetAll().FindAll(x => x.Type != ItemType.COIN);
        int index = 0;

        for (int i = 0; i < itemNavigations.Length; i++)
        {
            var navigation = itemNavigations[i];

            if (i < useItems.Count)
            {
                navigation.SetItem(useItems[i]);
            }
            else
                navigation.SetItem(null);
        }
    }

    public void CreateRandomItem()
    {
        var item = itemPool.Get();

        item.Initialize();
        item.SetItem((ItemType)Random.Range(0, (int)ItemType.COIN));
        item.transform.position = Utill.GetRandomPointBounds(mapBounds);
    }

    public void CreateItem(Vector3 position, ItemType itemType)
    {
        var item = itemPool.Get();

        item.Initialize();
        item.SetItem(itemType);
        item.transform.position = position;
    }

    public void DeleteItem(Item item)
    {
        itemPool.Delete(item);
    }
}
