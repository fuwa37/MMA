using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCanvas : MonoBehaviour {
	public GameObject start;
	public GameObject exit;
	private bool state=false;
	// Use this for initialization
	void Start () {
		start.SetActive (false);
		exit.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if ((!state) && (Input.GetKeyUp (KeyCode.Mouse0))) {
			state = true;
			start.SetActive (true);
			exit.SetActive (true);
		}
	}
}
