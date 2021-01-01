using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AdvancedComponentCopy : MonoBehaviour
{
    public static GameObject copyTarget;

    [MenuItem ("Tools/Advanced Component Copy/Copy All Components")]
    public static void CopyAllComponents()
    {
        GameObject target = Selection.gameObjects[0];
        if(target == null)
        {
            return;
        }
        copyTarget = target;
    }

    [MenuItem ("Tools/Advanced Component Copy/Paste All Components")]
    public static void PasteAllComponents()
    {
        foreach(GameObject target in Selection.gameObjects)
        {
            if(target == null || copyTarget == null)
            {
                return;
            }

            DestroyAllComponents(target);

            foreach(Component component in copyTarget.GetComponents<Component>())
            {
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
                if(component.GetType() == typeof(Transform))
                {
                    //UnityEditorInternal.ComponentUtility.PasteComponentValues(target.transform);
                    continue;
                }
                else
                {
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(target);
                }
            }
        }
    }

    public static void DestroyAllComponents(GameObject target)
    {
        foreach(Component component in target.GetComponents<Component>())
        {
            if(component.GetType() == typeof(Transform))
            {
                continue;
            }
            DestroyImmediate(component);
        }
    }

    [MenuItem ("Tools/Advanced Component Copy/Create Self Reference")]
    public static void CreateSelfReference()
    {
        Debug.Log(Selection.activeObject);
        /*GameObject target = Selection.gameObjects[0];
        if(target == null)
        {
            return;
        }
        copyTarget = target;*/
    }
}