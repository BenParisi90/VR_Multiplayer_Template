using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Target : MonoBehaviourPun
{
    public virtual void Select(Transform selector)
    {
        //Debug.Log("Target take damage " + name);
    }
}
