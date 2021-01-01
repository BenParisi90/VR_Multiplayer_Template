using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlickTurn : MonoBehaviour
{
    bool stickCentered;
    [SerializeField]
    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        stickCentered = true;
        enabled = playerInput.isRightHand;
    }

    // Update is called once per frame
    void Update()
    {
        if(stickCentered && playerInput.currentStickPosition != StickPosition.CENTER)
        {
            float turnAmount = playerInput.currentStickPosition == StickPosition.LEFT ? -45f : 45f;
            TurnPlayer(turnAmount);
            stickCentered = false;
        }
        else if(!stickCentered && playerInput.currentStickPosition == StickPosition.CENTER)
        {
            stickCentered = true;
        }
    }

    void TurnPlayer(float turnAmount)
    {
        AvatarManager.instance.localPlayerRoot.Rotate(0,turnAmount,0);
    }
}
