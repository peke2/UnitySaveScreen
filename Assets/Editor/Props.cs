using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Props
{
    [MenuItem("Props/persistentパスを開く")]
    public static void openPersistentPath()
    {
        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            System.Diagnostics.Process.Start(Application.persistentDataPath);
        }
    }
}