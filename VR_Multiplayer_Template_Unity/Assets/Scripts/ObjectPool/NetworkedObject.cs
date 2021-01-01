using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class NetworkedObject : MonoBehaviourPun
{
    [PunRPC]
    public void SetActive(bool active, float[] position, float rotation)
    {
        transform.position = DataConversion.FloatArrayToVector3(position);
        transform.rotation = Quaternion.identity;
        transform.RotateAround(transform.position, Vector3.up, rotation);
        gameObject.SetActive(active);
    }
}
