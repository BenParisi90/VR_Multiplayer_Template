using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FastNavigation : MonoBehaviour
{
    static string PROJECT = "UnityEditor.ProjectBrowser";
    static string HIERARCHY = "UnityEditor.SceneHierarchyWindow";
    static int numberOfControlGroups = 10;
    public static List<Object[]> controlGroups;


    static void RecordGroup(int groupIndex)
    {
        //populate the list of control groups if it is null
        if(controlGroups == null)
        {
            controlGroups = new List<Object[]>();
            for(int i = 0; i < numberOfControlGroups; i ++)
            {
                controlGroups.Add(null);
            }
        }
        //set the control group
        controlGroups[groupIndex] = Selection.objects;
    }



    [MenuItem("Tools/Fast Navigation/Select Project Root")]
    static void SelectProjectRoot()
    {
        FocusWindow(PROJECT);
        FileSystemInfo[] rootItems = new DirectoryInfo( Application.dataPath ).GetFileSystemInfos();
        Object rootAsset = AssetDatabase.LoadAssetAtPath<Object>("Assets");
        //run collapse command when selection is complete
        Selection.SetActiveObjectWithContext(rootAsset, null);
    }

    [MenuItem("Tools/Fast Navigation/Select Hierarchy Root")]
    static void SelectHierarchyRoot()
    {
        FocusWindow(HIERARCHY);
        GameObject[] rootSceneObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        Selection.activeGameObject = rootSceneObjects[0];
    }

    static void SelectGroup(int groupIndex)
    {
        Selection.objects = controlGroups[groupIndex];
        Selection.activeObject = controlGroups[groupIndex][0];

        if(Selection.activeObject.GetType() == typeof(GameObject))
        {
            FocusWindow(HIERARCHY);
        }
        else
        {
            FocusWindow(PROJECT);
        }
    }

    static void FocusWindow(string windowToFocus)
    {
        System.Type windowType = null;
        EditorWindow[] allWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
        foreach(EditorWindow window in allWindows)
        {
            if(window.GetType().ToString() == windowToFocus)
            {
                windowType = window.GetType();
                break;
            }
        }
        EditorWindow.FocusWindowIfItsOpen(windowType);
    }

    [MenuItem("Tools/Fast Navigation/Set Control Group 0")]
    public static void SetControlGroup0(){RecordGroup(0);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 0")]
    public static void SelectControlGroup0(){SelectGroup(0);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 1")]
    public static void SetControlGroup1(){RecordGroup(1);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 1")]
    public static void SelectControlGroup1(){SelectGroup(1);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 2")]
    public static void SetControlGroup2(){RecordGroup(2);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 2")]
    public static void SelectControlGroup2(){SelectGroup(2);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 3")]
    public static void SetControlGroup3(){RecordGroup(3);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 3")]
    public static void SelectControlGroup3(){SelectGroup(3);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 4")]
    public static void SetControlGroup4(){RecordGroup(4);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 4")]
    public static void SelectControlGroup4(){SelectGroup(4);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 5")]
    public static void SetControlGroup5(){RecordGroup(5);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 5")]
    public static void SelectControlGroup5(){SelectGroup(5);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 6")]
    public static void SetControlGroup6(){RecordGroup(6);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 6")]
    public static void SelectControlGroup6(){SelectGroup(6);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 7")]
    public static void SetControlGroup7(){RecordGroup(7);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 7")]
    public static void SelectControlGroup7(){SelectGroup(7);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 8")]
    public static void SetControlGroup8(){RecordGroup(8);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 8")]
    public static void SelectControlGroup8(){SelectGroup(8);}

    [MenuItem("Tools/Fast Navigation/Set Control Group 9")]
    public static void SetControlGroup9(){RecordGroup(9);}
    [MenuItem("Tools/Fast Navigation/Select Control Group 9")]
    public static void SelectControlGroup9(){SelectGroup(9);}
}