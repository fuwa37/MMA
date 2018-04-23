using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadDungeon : MonoBehaviour {
	public Button btn1;
	// Use this for initialization
	void Start () {
		Button btn2 = btn1.GetComponent<Button>();
		btn2.onClick.AddListener(LS);
	}

	// Update is called once per frame
	void Update () {

	}

	void LS(){
		SceneManager.LoadScene ("Assets/test/Dungeon");
	}
}