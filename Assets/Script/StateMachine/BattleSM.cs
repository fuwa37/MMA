using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BattleSM : MonoBehaviour
{
	public enum battleState{
		WAIT,
		ACTION,
		PERFORME,
	}

	public battleState bs;

	public List<TurnHandler> action=new List<TurnHandler> ();

	public List<GameObject> memeP=new List<GameObject> ();
	public List<GameObject> enemyE=new List<GameObject> ();


	// Use this for initialization
	void Start ()
	{
		Debug.Log ("aaaaaaa");
		bs = battleState.WAIT;
		memeP.AddRange(GameObject.FindGameObjectsWithTag("Meme"));
		enemyE.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(bs){
		case(battleState.WAIT):{
				Debug.Log (bs);
				break;
			}
		case(battleState.ACTION):{
				break;
			}
		case(battleState.PERFORME):{
				break;
			}
		}
	
	}

	public void collectAction(TurnHandler input){
		action.Add (input);
	}
}

