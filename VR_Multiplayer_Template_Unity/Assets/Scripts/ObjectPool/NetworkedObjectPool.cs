using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkedObjectPool : ObjectPool
{
    void Start(){}

    override public void PoolObjects()
    {
        for(int i = 0; i < numInPool; i ++)
        {
            GameObject newGameObject = null;
            if(PhotonNetwork.IsMasterClient)
            {
                newGameObject = PhotonNetwork.InstantiateRoomObject(prefab.name, Vector3.zero, Quaternion.identity);
            }
        }
    }

    override protected void ActivatePooledObject(GameObject targetGameObject, Vector3 position, Quaternion rotation)
    {
        float[] positionFloatArray = DataConversion.Vector3ToFloatArray(position);
        targetGameObject.GetComponent<NetworkedObject>().photonView.RPC("SetActive", RpcTarget.All, true, positionFloatArray, rotation.y);
    }
}
