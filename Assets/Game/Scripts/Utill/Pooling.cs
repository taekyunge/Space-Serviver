using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀링
/// </summary>
/// <typeparam name="T"></typeparam>
public class Pooling<T> where T : Component
{
    private T baseItem = null;
    private Transform root = null;

    private Queue<T> itemPool = new Queue<T>();
    private List<T> useItems = new List<T>();

    public int count { get { return useItems.Count; } }

    public Pooling(int poolingCount, T baseObj, Transform root)
    {
        baseItem = baseObj;
        this.root = root;

        for (int i = 0; i < poolingCount; i++)
        {
            Create(root);
        }

        Delete(baseObj);
    }

    private void Create(Transform root)
    {
        var obj = Object.Instantiate(baseItem, root);

        obj.gameObject.SetActive(false);

        itemPool.Enqueue(obj);
    }

    public void Delete(T obj)
    {
        if(root != null)
            obj.transform.SetParent(root);

        obj.gameObject.SetActive(false);

        useItems.Remove(obj);
        itemPool.Enqueue(obj);
    }

    public void DeleteAll()
    {
        for (int i = 0; i < useItems.Count; i++)
        {
            T obj = useItems[i];

            if (root != null)
                obj.transform.SetParent(root);

            obj.gameObject.SetActive(false);

            itemPool.Enqueue(obj);
        }

        useItems.Clear();
    }

    public T Get()
    {
        if (itemPool.Count == 0)
            Create(root);

        var obj = itemPool.Dequeue();

        obj.gameObject.SetActive(true);

        useItems.Add(obj);

        return obj;
    }

    public T Get(Vector3 position)
    {
        if (itemPool.Count == 0)
            Create(root);

        var obj = itemPool.Dequeue();

        obj.transform.position = position;
        obj.gameObject.SetActive(true);

        useItems.Add(obj);

        return obj;
    }

    public List<T> GetAll()
    {
        return useItems;
    }
}
