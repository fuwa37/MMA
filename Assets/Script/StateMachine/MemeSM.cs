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
	}

	public charState curState;

	public MemeSM ()
	{
	}

	void Start(){
		curState = charState.WAITING;
		BSM = GameObject.Find ("BattleSystem").GetComponent <BattleSM>();
	}

	void Update(){
		Debug.Log (curState);
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
		}
	}

	void selected(){
		TurnHandler atking = new TurnHandler ();
		atking.meme = meme.meme;
		atking.atk = this.gameObject;
	}
}

