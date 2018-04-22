using UnityEngine;
using System;

public class MemeSM : MonoBehaviour
{
	public Meme meme;
	public BattleSM BSM;

	public enum charState{
		ACTION,
		WAITING,
		SELECTED,
		DEAD,
		ADDTOLIST,
	}

	public charState curState;
	public GameObject Selector;

	public MemeSM ()
	{
	}

	void Start(){
		curState = charState.ADDTOLIST;
		Selector.SetActive (false);
		BSM = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent <BattleSM>();
	}

	void Update(){
		switch (curState) {
		case(charState.ACTION):
			{
				break;
			}
		case(charState.WAITING):
			{
				break;
			}
		case(charState.SELECTED):
			{
				break;
			}
		case(charState.DEAD):
			{
				break;
			}
		case(charState.ADDTOLIST):
			{
				BSM.playerMeme.Add (this.gameObject);
				curState = charState.WAITING;
				break;
			}
		}
	}

	void selected(){
		TurnHandler atking = new TurnHandler ();
		atking.meme = meme.meme;
		atking.atk = this.gameObject;
	}

	void OnMouseDown(){
		Debug.Log ("selected");
	}
}

