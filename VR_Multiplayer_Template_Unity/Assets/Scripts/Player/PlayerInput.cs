using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine;

public enum PlayerInputType
{
    PRIMARY_BUTTON,
    SECONDARY_BUTTON
}

public enum StickPosition
{
    LEFT,
    RIGHT,
    CENTER
}

public class PlayerInput : MonoBehaviour
{

    InputDevice device;
    [HideInInspector]
    public bool isRightHand;
    [HideInInspector]
    public bool triggerDown = false;
    [HideInInspector]
    public StickPosition currentStickPosition;
    float flickThreshold = .5f;
    [HideInInspector]
    public float triggerDownThreshold = .8f;
    [HideInInspector]
    public float triggerUpThreshold = .2f;
    public float triggerValue
    {
        get
        {
            float output = 0;
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out output);
            return output;
        }
    }

    void Update()
    {
        //for if the controller was off when the game started
        if(!device.isValid)
        {
            SetHand(isRightHand);
        }
        /*bool triggerValue;
        if(triggerDown && device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && !triggerValue)
        {
            triggerDown = false;
        }*/
        if(!triggerDown && triggerValue > triggerDownThreshold)
        {
            triggerDown = true;
        }
        else if(triggerDown && triggerValue < triggerUpThreshold)
        {
            triggerDown = false;
        }
        Vector2 axisValue;
        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out axisValue);
        float xAxis = axisValue[0];
        if(xAxis > flickThreshold)
        {
            currentStickPosition = StickPosition.RIGHT;
        }
        else if(xAxis < -flickThreshold)
        {
            currentStickPosition = StickPosition.LEFT;
        }
        else
        {
            currentStickPosition = StickPosition.CENTER;
        }
    }

    //does this gun belong in the left or right hand?
    public void SetHand(bool rightHand)
    {
        isRightHand = rightHand;
        var inputDevices = new List<InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(rightHand ? UnityEngine.XR.XRNode.RightHand : UnityEngine.XR.XRNode.LeftHand, inputDevices);
        if(inputDevices.Count > 0)
        {
            device = inputDevices[0];
            Debug.Log("Set Hand to device " + device);
        }
        if(inputDevices.Count > 1)
        {
            Debug.Log("Found more than one " + (rightHand ? "Right" : "Left") + " hand!");
        }
    }

    public bool GetButtonInput(PlayerInputType playerInputType)
    {
        bool returnValue = false;
        switch(playerInputType)
        {
            case PlayerInputType.SECONDARY_BUTTON:
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out returnValue);
                break;
            default:
                Debug.Log("Player input type not valid. Try using GetTriggerInput() instread?");
                break;
        }
        return returnValue;
    }


    public void Vibrate(float intensity, float duration)
    {
        if(!device.isValid)
        {
            return;
        }
        device.SendHapticImpulse(0, intensity, duration);
    }
}
