using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BattleSM : MonoBehaviour
{
	public enum battleState{
		WAIT,
		ACTION,
		PERFORM,
	}

	public enum playerState{
		WAITING,
		SELECT,
		INPUT,
		DONE,
	}

	public battleState bs;
	public playerState ps;

	public List<TurnHandler> action = new List<TurnHandler> ();

	public List<GameObject> memeP = new List<GameObject> ();
	public List<GameObject> enemyE = new List<GameObject> ();

	public List<GameObject> playerMeme = new List<GameObject> ();
	public TurnHandler playerHandler;

	public GameObject playerUI;
	public GameObject enemyUI;


	// Use this for initialization
	void Start ()
	{
		bs = battleState.WAIT;
		ps = playerState.SELECT;
		memeP.AddRange(GameObject.FindGameObjectsWithTag("Meme"));
		enemyE.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		playerUI.SetActive (false);
		enemyUI.SetActive (false);
	
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
				if (action[0].type=="Enemy"){
					EnemySM ESM = atkp.GetComponent <EnemySM> ();
					ESM.atktarget = action [0].def;
					ESM.curState = EnemySM.enemyState.ACTION;
				}

				bs = battleState.PERFORM;
				break;
			}
		case(battleState.PERFORM):{
				break;
			}
		}

		switch(ps){
		case(playerState.WAITING):{
				break;
			}
		case(playerState.SELECT):{
				if (playerMeme.Count>0){
					playerMeme [0].transform.Find ("Selector").gameObject.SetActive (true);
					playerUI.SetActive (true);
					ps = playerState.WAITING;
				}
				break;
			}
		case(playerState.INPUT):{
				break;
			}
		case(playerState.DONE):{
				break;
			}
		}
	
	}

	public void collectAction(TurnHandler input){
		action.Add (input);
	}
}

