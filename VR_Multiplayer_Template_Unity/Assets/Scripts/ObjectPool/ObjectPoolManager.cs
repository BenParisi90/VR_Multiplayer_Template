using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    Dictionary<string, ObjectPool> objectPools;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        objectPools = new Dictionary<string, ObjectPool>();
        ObjectPool[] childObjectPools = GetComponentsInChildren<ObjectPool>();
        foreach(ObjectPool objectPool in childObjectPools)
        {
            objectPools.Add(objectPool.prefab.name, objectPool);
        }
        if(PhotonNetwork.InRoom)
        {
            PoolNetworkedObjects();
        }
    }

    public GameObject ActivateNext(string prefabName, Vector3 position, Quaternion rotation)
    {
        ObjectPool targetObjectPool;
        objectPools.TryGetValue(prefabName, out targetObjectPool);
        if(targetObjectPool != null)
        {
            return targetObjectPool.ActivateNext(position, rotation);
        }
        Debug.Log(string.Format("Can't find the required {0} in any pool.", prefabName));
        return null;
    }

    public void PoolNetworkedObjects()
    {
        foreach(ObjectPool objectPool in objectPools.Values)
        {
            if(objectPool.networked)
            {
                objectPool.PoolObjects();
            }
        }
    }

    public void AddToPool(GameObject targetObject, string prefabName)
    {
        objectPools[prefabName].AddToPool(targetObject);
    }

    public void ClearAll()
    {
        foreach(ObjectPool objectPool in objectPools.Values)
        {
            objectPool.Clear();
        }
    }

    public bool PoolsPopulated()
    {
        foreach(ObjectPool objectPool in objectPools.Values)
        {
            if(objectPool.pool[objectPool.pool.Count - 1] == null)
            {
                return false;
            }
        }
        return true;
    }
}