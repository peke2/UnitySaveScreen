using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;


public class Capture : MonoBehaviour {
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void SaveToCameraRoll(string path, string cbGameObjectName, string cbMethodName);
#endif


    string baseName = "testshot";

	// Use this for initialization
	void Start () {
        var button = GetComponent<Button>();
        button.onClick.AddListener(onButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void onButton()
    {
        //ScreenCapture.CaptureScreenshot();
        StartCoroutine(procSave());
    }


    GameObject getHideTarget()
    {
        return GameObject.Find("InvisibleSphere");
    }


    IEnumerator procSave()
    {
        var now = System.DateTime.Now;
        var name = baseName + now.ToString("_yyyyMMdd_HHmmss");
#if UNITYEDITOR || UNITY_EDITOR_OSX
        var path = Path.Combine(Application.persistentDataPath, name);
        var outputPath = path;
#else
        //  On mobile platforms the filename is appended to the persistent data path.
        var path = name;
        var outputPath = Path.Combine(Application.persistentDataPath, name);
#endif
        var hideTarget = getHideTarget();

        Debug.Log("パス["+path+"]");

        string[] files = Directory.GetFiles(Application.persistentDataPath);
        Debug.Log(">>>>"+Application.persistentDataPath);
        foreach(var file in files)
        {
            Debug.Log(file);
        }
        Debug.Log("<<<<");

        hideTarget.SetActive(false);

        ScreenCapture.CaptureScreenshot(path);

        //  このタイミングでアクティブに戻すとキャプチャ内に残る
        //  そもそも同じフレーム内だから状態変わらないから？
        //hideTarget.SetActive(true);

        while (!File.Exists(outputPath)) yield return null;

        //  キャプチャ終了後に戻すが、対象のオブジェクトが一瞬ちらつくのが気になる
        hideTarget.SetActive(true);

        Debug.Log("キャプチャ完了");

        //  カメラロールに保存
#if UNITY_IOS
        //SaveToCameraRoll(outputPath, "ButtonCapture", "saveCompleteCallback");
        SaveToCameraRoll(outputPath, gameObject.name, "saveCompleteCallback");
#endif
        Debug.Log("カメラロールへの保存完了");

        //  対象のファイルを消す
        //File.Delete(outputPath);
        //Debug.Log("一時保存されたファイルを削除");
    }

    void saveCompleteCallback(string path)
    {
        Debug.Log("削除対象["+path+"]");
        if (!File.Exists(path))
        {
            Debug.Log("ファイルが無いので削除中止");
            return;
        }
        File.Delete(path);
        Debug.Log("ファイルの削除完了");
    }
}
