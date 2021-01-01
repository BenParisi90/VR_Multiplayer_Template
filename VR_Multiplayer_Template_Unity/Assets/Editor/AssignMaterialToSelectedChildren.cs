using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssignMaterialToSelectedChildren : EditorWindow
{
    [SerializeField] private Material material;

    [MenuItem("Tools/Assign Material To Selected Children")]
    static public void main()
    {
        EditorWindow.GetWindow<AssignMaterialToSelectedChildren>();
    }

    private void OnGUI()
    {
        material = (Material)EditorGUILayout.ObjectField("Material", material, typeof(Material), false);

        if (GUILayout.Button("Assign"))
        {
            var obj = Selection.gameObjects;
            if(obj!=null)
            {
                for (int i = 0; i < obj.Length; i++)
                {
                    AssignMaterialRecursive(obj[i].transform, material);
                    EditorUtility.SetDirty(obj[i]);
                }
            }
            else
            {
                Debug.Log("Nothing selected");
            }
        }
    }

    void AssignMaterialRecursive(Transform target, Material material)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if(renderer != null)
        {
            Material[] materialArray = new Material[renderer.sharedMaterials.Length];
            for(int i = 0; i < materialArray.Length; i ++)
            {
                materialArray[i] = material;
            }
            renderer.sharedMaterials = materialArray;
        }
        for(int j = 0; j < target.childCount; j ++)
        {
            AssignMaterialRecursive(target.GetChild(j), material);
        }
    }
}