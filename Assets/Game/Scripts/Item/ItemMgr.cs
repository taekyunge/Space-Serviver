using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemMgr : Singleton<ItemMgr>
{
    private Pooling<Item> itemPool;
    private List<Item> usedRandomItems = new List<Item>();

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

        for (int i = 0; i < itemNavigations.Length; i++)
        {
            var navigation = itemNavigations[i];

            if (i < usedRandomItems.Count)
            {
                var item = usedRandomItems[i];

                item.Navigation = navigation;

                navigation.SetItem(item);
            }
        }
    }

    public void CreateRandomItem()
    {
        var item = itemPool.Get();

        item.Initialize();
        item.SetItem((ItemType)Random.Range(0, (int)ItemType.COIN));
        item.transform.position = Utill.GetRandomPointBounds(mapBounds);

        usedRandomItems.Add(item);
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
        if (item.Navigation != null)
        {
            usedRandomItems.Remove(item);

            item.Navigation.TargetItem = null;
            item.Navigation = null;
        }

        itemPool.Delete(item);
    }
}
