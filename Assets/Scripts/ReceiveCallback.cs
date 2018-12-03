using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ReceiveCallback : MonoBehaviour {
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void CallUnity(string cbGameObjectName, string cbMethodName);
#endif

    // Use this for initialization
    void Start () {
#if UNITY_IOS
        CallUnity("ReceiveCallback", "called");
#endif
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void called(string message)
    {
        Debug.Log("プラグインからのコールバック->"+message);
    }
}
