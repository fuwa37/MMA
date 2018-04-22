using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class MemeSM : MonoBehaviour
{
	public Meme meme;
	public BattleSM BSM;
	public bool isSelected=false;

	public enum charState{
		PROCESSING,
		ACTION,
		WAITING,
		SELECTED,
		DEAD,
		ADDTOLIST,
	}

	public charState curState;
	public GameObject Selector;
	public Image probar;

	private float cur_cooldown = 0f;
	private float max_cooldown = 5f;
	public Vector3 ipos;
	private bool action=false;
	public GameObject atktarget;
	private float animSpeed=4f;

	public MemeSM ()
	{
	}

	void Start(){
		curState = charState.PROCESSING;
		Selector.SetActive (false);
		ipos = transform.position;
		BSM = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent <BattleSM>();
	}

	void Update(){
		switch (curState) {
		case(charState.PROCESSING):{
				updatebar ();
				break;
			}
		case(charState.ACTION):
			{
				StartCoroutine ((aksi()));
				break;
			}
		case(charState.WAITING):
			{
				if (isSelected){
					curState = charState.ACTION;
				}
				break;
			}
		case(charState.SELECTED):
			{
				selected ();
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

	private IEnumerator aksi(){
		if(action){
			yield break;
		}

		action = true;

		Vector3 targetpos = new Vector3(atktarget.transform.position.x-1.5f,atktarget.transform.position.y,atktarget.transform.position.z);

		while(movetarget(targetpos)){
			yield return null;
		}

		yield return new WaitForSeconds (0.5f);

		Vector3 spos = ipos; 

		while(moveipos(spos)){
			yield return null;
		}

		BSM.action.RemoveAt (0);
		BSM.bs = BattleSM.battleState.WAIT;

		action = false;
		isSelected = false;
		curState = charState.WAITING;
	}

	void selected(){
		TurnHandler atking = new TurnHandler ();
		atking.meme = meme.meme;
		atking.type = "Musuh";
		atking.atk = this.gameObject;
		atking.def = BSM.enemyE [Random.Range (0, BSM.enemyE.Count)];
		Debug.Log (atking.def);
		BSM.collectAction (atking);
	}

	void OnMouseDown(){
		Selector.SetActive (true);
		isSelected = true;
	}

	private bool movetarget(Vector3 target){
		return target != (transform.position = Vector3.MoveTowards (transform.position, target, animSpeed * Time.deltaTime));
	}

	private bool moveipos(Vector3 target){
		return target != (transform.position = Vector3.MoveTowards (transform.position, target, animSpeed * Time.deltaTime));
	}

	void updatebar(){
		cur_cooldown = cur_cooldown + Time.deltaTime;
		float calc_cooldown = cur_cooldown / max_cooldown;
		probar.transform.localScale = new Vector3 (Mathf.Clamp (calc_cooldown, 0, 1), probar.transform.localScale.y, probar.transform.localScale.z);
		if (cur_cooldown>max_cooldown){
			curState = charState.ADDTOLIST;
		}
	}
}

