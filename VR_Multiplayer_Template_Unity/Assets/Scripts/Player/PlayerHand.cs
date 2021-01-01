using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHand : MonoBehaviourPun
{
    public PlayerInput playerInput;


    [SerializeField]
    Animator animator;

    public int playerNum = -1;

    void Start()
    {
    }

    [PunRPC]
    public void Initialize(int newPlayerNum, bool rightHand)
    {
        AvatarManager.instance.playerHands[newPlayerNum].hands[rightHand?0:1] = this;
        playerNum = newPlayerNum;
    }
}