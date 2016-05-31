using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {

	// Use this for initialization
	void Start () {

			//This code is run on the server
			uLink.Network.InitializeServer(32, 7100);
		}

	
	// Update is called once per frame
	void Update () {
	
	}

	void uLink_OnServerInitialized() {
		Debug.Log("Server successfully started");
	}
	void uLink_OnConnectedToServer () {
		Debug.Log("Now connected to server");
		Debug.Log("Local Port = " + uLink.Network.player.port.ToString());
	}
	void uLink_OnPlayerDisconnected(uLink.NetworkPlayer player)
	{
		uLink.Network.DestroyPlayerObjects(player);
		uLink.Network.RemoveRPCs(player);
	}
	void uLink_OnFailedToConnect(uLink.NetworkConnectionError error)
	{
		Debug.LogError("uLink got error: "+ error);
	}
	void uLink_OnPlayerConnected(uLink.NetworkPlayer player) {
		Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
	}
}
