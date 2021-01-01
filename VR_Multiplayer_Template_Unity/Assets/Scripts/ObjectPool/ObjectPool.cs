using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pool;
    public GameObject prefab;
    public GameObject prefab_Quest1;
    GameObject prefabInUse;
    public int numInPool;
    public bool networked;

    void Start()
    {
        PoolObjects();
    }

    public virtual void PoolObjects()
    {
        pool = new List<GameObject>();
        prefabInUse = prefab_Quest1 != null && SessionInfo.deviceName == SessionInfo.QUEST1_DEVICE_NAME ? prefab_Quest1 : prefab;
        for(int i = 0; i < numInPool; i ++)
        {
            GameObject newGameObject = null;
            newGameObject = Instantiate(prefabInUse, Vector3.zero, Quaternion.identity, transform);
            if(newGameObject != null)
            {
                pool.Add(newGameObject);
                newGameObject.SetActive(false);
            }
        }
    }

    public GameObject ActivateNext(Vector3 position, Quaternion rotation)
    {
        foreach(GameObject pooledGameObject in pool)
        {
            if(!pooledGameObject.activeInHierarchy)
            {
                ActivatePooledObject(pooledGameObject, position, rotation);
                return pooledGameObject;
            }                
        }
        Debug.Log("could not find unused pooled object");
        return null;
    }

    protected virtual void ActivatePooledObject(GameObject targetPooledObject, Vector3 position, Quaternion rotation)
    {
        targetPooledObject.transform.position = position;
        targetPooledObject.transform.rotation = rotation;
        targetPooledObject.SetActive(true);
    }

    public void AddToPool(GameObject targetGameObject)
    {
        targetGameObject.transform.parent = transform;
        pool.Add(targetGameObject);
    }

    public void Clear()
    {
        foreach(GameObject pooledGameObject in pool)
        {
            pooledGameObject.SetActive(false);
        }
    }
}