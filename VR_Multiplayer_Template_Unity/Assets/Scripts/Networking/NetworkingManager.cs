using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public enum ConnectionType 
{
	JOIN_OR_CREATE_RANDOM,
	CREATE_PRIVATE,
    JOIN_PRIVATE
}

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    public static NetworkingManager instance;
    [SerializeField]
    StartSceneMenuController startSceneMenuController;
    [SerializeField]
    TextMeshPro feedbackText;
    string gameVersion = "0.0.1";
    public static byte maxPlayersPerRoom = 2;
    public static ConnectionType connectionType = ConnectionType.CREATE_PRIVATE;
    int randomNameLength = 5;
    string nextRoomName = "";
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        instance = this;
        PhotonNetwork.GameVersion = gameVersion;
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
		else
		{
			if(PhotonNetwork.InRoom)
			{
                startSceneMenuController.InitializeRoom();
			}
			else if(PhotonNetwork.InLobby)
			{
				CreateNewPrivateRoom();
			}
		}
	}

    public override void OnConnectedToMaster()
	{
        LogFeedback(SessionInfo.deviceName);
		LogFeedback("Connected. Room type: " + connectionType);
		switch(connectionType)
		{
            case ConnectionType.CREATE_PRIVATE:
                CreateNewPrivateRoom();
                break;
			case ConnectionType.JOIN_OR_CREATE_RANDOM:
                PhotonNetwork.JoinRandomRoom();
                break;
            case ConnectionType.JOIN_PRIVATE:
				PhotonNetwork.JoinRoom(nextRoomName);
				StartCoroutine(JoinRoomTimeout());
                break;
		}
	}

    public void CreateNewPrivateRoom()
    {
        string startingRoomName = CreateRandomRoomName();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayersPerRoom;
        roomOptions.IsVisible = false;
        PhotonNetwork.CreateRoom(startingRoomName, roomOptions);
    }

    public override void OnDisconnected(DisconnectCause cause)
	{
		LogFeedback("Disconnected! "+cause);
		PhotonNetwork.OfflineMode = true;
        startSceneMenuController.ReturnToMainMenu();
	}

    public void ChangeConnectionType(ConnectionType newConnectionType)
    {
        LogFeedback("Change Connection Type: " + newConnectionType);
        connectionType = newConnectionType;
        PhotonNetwork.LeaveRoom();
    }

    public void JoinFriendsGame(string roomName)
	{
        LogFeedback("Join friends game: " + roomName);
		nextRoomName = roomName;
		ChangeConnectionType(ConnectionType.JOIN_PRIVATE);
	}

    IEnumerator JoinRoomTimeout()
	{
		yield return new WaitForSeconds(1.5f);
		if(!PhotonNetwork.InRoom)
		{
			LogFeedback("Room not found timeout");
            CreateNewPrivateRoom();
		}
	}

    public override void OnJoinedRoom()
	{
		LogFeedback("Joined room: " + (connectionType == ConnectionType.JOIN_OR_CREATE_RANDOM ? "Random" : PhotonNetwork.CurrentRoom.Name));
        if(PhotonNetwork.IsMasterClient)
        {
            SessionInfo.playerList = new int[]{PhotonNetwork.LocalPlayer.ActorNumber, -1, -1, -1};
            ObjectPoolManager.instance.PoolNetworkedObjects();
            photonView.RPC("SetPlayerListAndInitializeRoom", RpcTarget.All, SessionInfo.playerList);
        }
        
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		LogFeedback("Could not find empty room. Creating a new one.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
	}

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        LogFeedback("New player entered room");
        UpdateRoomPlayerTotal();
        if(PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < maxPlayersPerRoom; i ++)
            {
                if(SessionInfo.playerList[i] == -1)
                {
                    SessionInfo.playerList[i] = newPlayer.ActorNumber;
                    break;
                }
            }
            photonView.RPC("SetPlayerListAndInitializeRoom", RpcTarget.All, SessionInfo.playerList);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        LogFeedback("Player left room");
        UpdateRoomPlayerTotal();
        if(PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < maxPlayersPerRoom; i ++)
            {
                if(SessionInfo.playerList[i] == otherPlayer.ActorNumber)
                {
                    SessionInfo.playerList[i] = -1;
                    break;
                }
            }
            photonView.RPC("SetPlayerListAndInitializeRoom", RpcTarget.All, SessionInfo.playerList);
        }
    }

    void UpdateRoomPlayerTotal()
    {
        startSceneMenuController.UpdatePlayersInRoomText();
        PhotonNetwork.CurrentRoom.IsVisible = PhotonNetwork.CurrentRoom.PlayerCount < maxPlayersPerRoom;
    }

    public string CreateRandomRoomName()
	{
		string name = "";

		for (int counter = 1; counter <= randomNameLength; ++counter)
		{
			int rand = UnityEngine.Random.Range(65, 91);

			name += (char)rand;
		}

		Debug.Log("Room name generated: " + name);
		return name;
	}

    [PunRPC]
    public void SetPlayerListAndInitializeRoom(int[] actorNumbers)
    {
        SessionInfo.playerList = actorNumbers;
        if(AvatarManager.instance.localPlayerTarget == null)
        {
            startSceneMenuController.InitializeRoom();
        }
    }

    void LogFeedback(string feedback)
    {
        feedbackText.text += "\n" + feedback;
        Debug.Log(feedback);
    }
}
