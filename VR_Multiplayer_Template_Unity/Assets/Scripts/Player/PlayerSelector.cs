using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    public PlayerInput playerInput;
    public Transform shootTransform;
    bool waitingForPress = true;
    // Update is called once per frame
    void Update()
    {
        if(playerInput.triggerDown && waitingForPress)
        {
            waitingForPress = false;
            RaycastHit hit;
            if(Physics.Raycast(shootTransform.position, shootTransform.forward, out hit))
            {
                Target target = hit.transform.GetComponent<Target>();
                if(target != null)
                {
                    target.Select(transform);
                }

                Button button = hit.transform.GetComponent<Button>();
                if(button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
        else if(!playerInput.triggerDown)
        {
            waitingForPress = true;
        }
    }
}
