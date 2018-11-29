using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;


public class Capture : MonoBehaviour {
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void SaveToCameraRoll(string path);
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
        var path = Path.Combine(Application.persistentDataPath, name);

        var hideTarget = getHideTarget();

        hideTarget.SetActive(false);

        ScreenCapture.CaptureScreenshot(path);

        //  このタイミングでアクティブに戻すとキャプチャ内に残る
        //  そもそも同じフレーム内だから状態変わらないから？
        //hideTarget.SetActive(true);

        while (!File.Exists(path)) yield return null;

        //  キャプチャ終了後に戻すが、対象のオブジェクトが一瞬ちらつくのが気になる
        hideTarget.SetActive(true);

        Debug.Log("キャプチャ完了");

        //カメラロールに保存
#if UNITY_IOS
        SaveToCameraRoll(name);
#endif
        //対象のファイルを消す
    }

}
