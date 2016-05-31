using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void uLink_OnConnectedToServer()
	{
		uLink.Network.Instantiate (Player, transform.position, transform.rotation, 0);
	}
}
