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
		//Debug.Log (action.Count);
		switch(bs){
		case(battleState.WAIT):{
				if (action.Count>0){
					bs = battleState.ACTION;
				}
				break;
			}
		case(battleState.ACTION):{
				GameObject atkp = GameObject.Find(action[0].meme);
				Debug.Log (action[0].meme);
				Debug.Log (action[0].atk);
				Debug.Log (action[0].def);
				Debug.Log (atkp);
				if (action[0].type=="Enemy"){
					EnemySM ESM = atkp.GetComponent <EnemySM> ();
					ESM.atktarget = action [0].def;
					ESM.curState = EnemySM.enemyState.ACTION;
				}
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

