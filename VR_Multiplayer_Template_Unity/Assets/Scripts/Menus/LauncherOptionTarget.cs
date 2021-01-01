using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuOption {
    PLAY_SOLO,
    PLAY_WITH_RANDOS,
    PLAY_WITH_FRIENDS,
    BACK,
    Count
}

public class LauncherOptionTarget : Target
{
    public MenuOption option;

    override public void Select(Transform attacker)
    {
        StartCoroutine(SelectOptionNextFrame());
    }

    IEnumerator SelectOptionNextFrame()
    {
        yield return new WaitForEndOfFrame();
        StartSceneMenuController.instance.SelectOption(option);
    }
}
