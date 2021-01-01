using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class StartSceneMenuController : MonoBehaviourPun
{
    public static StartSceneMenuController instance;

    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject joinFriendsRoomMenu;
    [SerializeField]
    GameObject backButton;
    [SerializeField]
    List<Transform> playerPositions;
    [SerializeField]
    GameObject roomName;
    [SerializeField]
    TextMeshPro roomNameText;
    [SerializeField]
    TextMeshPro playersInRoomText;
    [SerializeField]
    AvatarManager avatarManager;
    [SerializeField]
    List<GameObject> multiplayerMenus;

    void Start()
    {
        instance = this;
        Debug.Log("PhotonNetwork.InRoom " + PhotonNetwork.InRoom);
        Debug.Log("PhotonNetwork.IsConnected " + PhotonNetwork.IsConnected);
    }

    public void ActivateMultiplayerMenu(GameObject targetMenu)
    {
        Debug.Log("ActivateCenterMenu " + targetMenu.name);
        foreach(GameObject menu in multiplayerMenus)
        {
            menu.SetActive(menu == targetMenu);
        }
        backButton.SetActive(targetMenu != mainMenu);
        roomName.SetActive(NetworkingManager.connectionType != ConnectionType.JOIN_OR_CREATE_RANDOM);
    }

    public void SelectOption(MenuOption option)
    {
        switch(option)
        {
            case MenuOption.PLAY_WITH_RANDOS:
                NetworkingManager.instance.ChangeConnectionType(ConnectionType.JOIN_OR_CREATE_RANDOM);
                break;
            case MenuOption.PLAY_WITH_FRIENDS:
                ActivateMultiplayerMenu(joinFriendsRoomMenu);
                break;
            case MenuOption.BACK:
                backButton.SetActive(false);
                if(joinFriendsRoomMenu.activeInHierarchy)
                {
                    ActivateMultiplayerMenu(mainMenu);
                }
                else
                {
                    NetworkingManager.instance.ChangeConnectionType(ConnectionType.CREATE_PRIVATE);
                }
                break;
        }
    }

    public void InitializeRoom()
    {
        Debug.Log("Initialize Room " + NetworkingManager.connectionType);
        avatarManager.CreateLocalAvatar(SessionInfo.localPlayerNum);
        avatarManager.localPlayerRoot.position = playerPositions[SessionInfo.localPlayerNum].position;
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        switch(NetworkingManager.connectionType)
        {
            case ConnectionType.CREATE_PRIVATE:
                ActivateMultiplayerMenu(mainMenu);
                break;
            case ConnectionType.JOIN_OR_CREATE_RANDOM:
            case ConnectionType.JOIN_PRIVATE:
                UpdatePlayersInRoomText();
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        ActivateMultiplayerMenu(mainMenu);
    }

    public void UpdatePlayersInRoomText()
    {
        ActivateMultiplayerMenu(playersInRoomText.gameObject);
        int playersInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        playersInRoomText.text = string.Format("Players In Room: {0}/{1}", playersInRoom, NetworkingManager.maxPlayersPerRoom);
    }
}
