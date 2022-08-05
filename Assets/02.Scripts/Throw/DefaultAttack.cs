using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAttack : MonoBehaviour
{
    public static DefaultAttack Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<Bullet> poolingObjectQueue = new Queue<Bullet>();

    private void Awake()
    {
        Instance = this;
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Bullet CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    /// <summary>
    /// 총알을 꺼내옵니다
    /// </summary>
    /// <returns></returns>
    public static Bullet GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }
    /// <summary>
    /// 총알을 삭제합니다
    /// </summary>
    /// <param name="obj">삭제할 오브젝트</param>
    public static void ReturnObject(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
