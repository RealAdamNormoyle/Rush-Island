using UnityEngine;
using System.Collections;

public class RPCTest : uLink.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (20, 170, 80, 50), "Send RPC")) {
			networkView.RPC ("GetRPC", uLink.RPCMode.AllBuffered);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	[RPC]
	void GetRPC()
	{
		Debug.Log ("get rpc");
	}
}
