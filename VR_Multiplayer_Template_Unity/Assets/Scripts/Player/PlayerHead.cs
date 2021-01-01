using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHead : Target
{
    public int playerNum;

    public GameObject fadeEffect;

    Transform torso;

    [PunRPC]
    public void Initialize(int targetPlayer)
    {
        AvatarManager.instance.playerTargets[targetPlayer] = this;
        playerNum = targetPlayer;
    }

    public void SetTorso(GameObject targetTorso)
    {
        torso = targetTorso.transform;
    }

    void Update()
    {
        if(torso != null)
        {
            torso.position = transform.position;
            torso.rotation = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0);
        }
    }
}
