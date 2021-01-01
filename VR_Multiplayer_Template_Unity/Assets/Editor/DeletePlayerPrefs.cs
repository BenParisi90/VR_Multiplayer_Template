using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeletePlayerPrefs : MonoBehaviour
{
    [MenuItem("Tools/Delete Player Prefs")]
    static public void deletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
