using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoSomethingWithSelected : MonoBehaviour
{
    [MenuItem("Tools/Do Something With Selected")]
    static public void main()
    {
        string log = "";
        var obj = Selection.gameObjects;
        if(obj!=null)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                EditorUtility.SetDirty(obj[i]);
            }
            Debug.Log(log);
        }
        else
        {
            Debug.Log("Nothing selected");
        }
    }
}