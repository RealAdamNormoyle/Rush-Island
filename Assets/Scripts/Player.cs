using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : uLink.MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (networkView.isMine) {
			gameObject.GetComponent<FirstPersonController> ().enabled = true;
			gameObject.GetComponent<FirstPersonController> ().kamera.enabled = true;
		} else {
			gameObject.GetComponent<FirstPersonController> ().enabled = false;
			gameObject.GetComponent<FirstPersonController> ().kamera.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
