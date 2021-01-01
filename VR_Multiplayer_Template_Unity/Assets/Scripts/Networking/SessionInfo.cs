using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;

public static class SessionInfo
{
    public static string QUEST1_DEVICE_NAME = "Oculus Quest";
    public static int[] playerList = new int[]{-1,-1,-1,-1};
    public static int localPlayerNum {get{return Array.IndexOf(playerList, PhotonNetwork.LocalPlayer.ActorNumber);}}
    public static InputDevice headset {get{return UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.Head);}}
    //UnityEngine.XR.InputDevices.GetDevicesAtXRNode(rightHand ? UnityEngine.XR.XRNode.RightHand : UnityEngine.XR.XRNode.LeftHand, inputDevices);
    public static string deviceName = headset.name;
}