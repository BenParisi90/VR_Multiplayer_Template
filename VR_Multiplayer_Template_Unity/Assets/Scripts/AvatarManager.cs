using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class AvatarManager : MonoBehaviourPun
{
    public static AvatarManager instance;
    public Transform localPlayerRoot;
    public PlayerHead localPlayerTarget;
    public PlayerHand localPlayerLeftHand;
    public PlayerHand localPlayerRightHand;
    public Transform leftHandAnchor;
    public Transform rightHandAnchor;
    public Transform headAnchor;
    public GameObject headPrefab;
    public GameObject handPrefab;
    public GameObject torsoPrefab;
    public List<PlayerNumberedAssets> playerNumberedAssets;

    public List<PlayerHead> playerTargets;
    public List<PlayerTorso> playerTorsos;
    public List<PlayerHandsList> playerHands;

    void OnEnable()
    {
        instance = this;
    }

    public void CreateLocalAvatar(int playerNum)
    {
        GameObject head = PhotonNetwork.Instantiate(this.headPrefab.name, headAnchor.position, headAnchor.rotation, 0);
        head.transform.parent = headAnchor;

        localPlayerTarget = head.GetComponent<PlayerHead>();
        localPlayerTarget.photonView.RPC("Initialize", RpcTarget.All, playerNum);

        localPlayerLeftHand = CreateHand(leftHandAnchor, false, playerNum);
        localPlayerRightHand = CreateHand(rightHandAnchor, true, playerNum);

        GameObject torso = PhotonNetwork.Instantiate(torsoPrefab.name, head.transform.position, Quaternion.identity);
        localPlayerTarget.SetTorso(torso);
        torso.GetComponent<PlayerTorso>().photonView.RPC("Initialize", RpcTarget.All, playerNum);

        photonView.RPC("SyncPlayerInfoWithMaster", RpcTarget.All);
    }
    
    public void PositionPlayer(int playerNum, Vector3 position)
    {
        localPlayerRoot.position = position;
    }

    public int NumLivingPlayers()
    {
        int numLivingPlayers = 0;
        foreach(PlayerHead playerTarget in playerTargets)
        {
            if(playerTarget != null)
            {
                numLivingPlayers ++;
            }
        }
        return numLivingPlayers;
    }

    public bool AllPlayersInitialized()
    {
        bool allPlayersInitialized = true;
        for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i ++)
        {
            if(playerTargets[i] == null)
            {
                allPlayersInitialized = false;
            }
        }
        return allPlayersInitialized;
    }

    PlayerHand CreateHand(Transform targetParent, bool rightHand, int playerNum)
    {
        GameObject newHandGameObject = PhotonNetwork.Instantiate(handPrefab.name, targetParent.position, targetParent.rotation, 0);
        newHandGameObject.transform.parent = targetParent;
        PlayerHand newHand = newHandGameObject.GetComponent<PlayerHand>();
        newHand.playerInput.SetHand(rightHand);
        newHand.playerNum = playerNum;
        newHand.photonView.RPC("Initialize", RpcTarget.All, playerNum, rightHand);
        return newHand;
    }

    [PunRPC]
    public void SyncPlayerInfoWithMaster()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        int i = 0;
        int j = 0;
        for(i = 0; i < playerTargets.Count; i ++)
        {
            if(playerTargets[i] != null)
            {
                playerTargets[i].photonView.RPC("Initialize", RpcTarget.All, i);
            }
        }
        for(i = 0; i < playerTorsos.Count; i ++)
        {
            if(playerTorsos[i] != null)
            {
                playerTorsos[i].photonView.RPC("Initialize", RpcTarget.All, i);
            }
        }
        for(i = 0; i < playerHands.Count; i ++)
        {
            for(j = 0; j < playerHands[i].hands.Count; j ++)
            {
                PlayerHand weapon = playerHands[i].hands[j];
                if(weapon != null)
                {
                    weapon.photonView.RPC("Initialize", RpcTarget.All, i, j==0);
                }
            }
        }
        for(i = 0; i < playerTargets.Count; i ++)
        {
            if(playerTargets[i] != null)
            {
                photonView.RPC("SetPlayerColor", RpcTarget.All, i);
            }
        }
    }

    [PunRPC]
    public void SetPlayerColor(int targetPlayer)
    {
        SetColor(playerTargets[targetPlayer].transform, targetPlayer);
        SetColor(playerTorsos[targetPlayer].transform, targetPlayer);
        SetColor(playerHands[targetPlayer].hands[0].transform, targetPlayer);
        SetColor(playerHands[targetPlayer].hands[1].transform, targetPlayer);
    }

    void SetColor(Transform root, int targetPlayer)
    {
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            renderer.material = playerNumberedAssets[targetPlayer].playerMaterial;
        }
    }
}

[System.Serializable]
 public class PlayerHandsList
 {
      public List<PlayerHand> hands;
 }

[System.Serializable]
public class PlayerNumberedAssets
{
    public Material playerMaterial;
}