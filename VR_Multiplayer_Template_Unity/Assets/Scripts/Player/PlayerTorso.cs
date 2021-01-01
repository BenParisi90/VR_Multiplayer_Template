using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerTorso : MonoBehaviourPun
{
    [PunRPC]
    public void Initialize(int targetPlayer)
    {
        AvatarManager.instance.playerTorsos[targetPlayer] = this;
    }
}
