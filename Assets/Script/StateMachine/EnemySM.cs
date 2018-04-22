using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class EnemySM : MonoBehaviour
{
	public Enemy enemy;
	private BattleSM BSM;

	public enum enemyState{
		PROCESSING,
		ACTION,
		WAITING,
		CHOOSE,
		DEAD,
	}

	public enemyState curState;
	public Image probar;

	private float cur_cooldown = 0f;
	private float max_cooldown = 5f;
	public Vector3 ipos;
	private bool action=false;
	public GameObject atktarget;
	private float animSpeed=4f;

	public EnemySM ()
	{
	}

	void Start(){
		curState = enemyState.PROCESSING;
		BSM = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent <BattleSM>();
		ipos = transform.position;

	}

	void Update(){
		switch (curState) {
		case(enemyState.PROCESSING):{
				updatebar ();
				break;
			}
		case(enemyState.ACTION):
			{
				//StartCoroutine (aksi ());
				break;
			}
		case(enemyState.WAITING):
			{
				waiting ();
				break;
			}
		case(enemyState.CHOOSE):
			{
				chooseact ();
				curState = enemyState.ACTION;
				break;
			}
		case(enemyState.DEAD):
			{
				//Debug.Log ("DEAD");
				break;
			}
		}
	}

	void waiting(){
		curState = enemyState.CHOOSE;
	}

	void chooseact(){
		TurnHandler atking = new TurnHandler ();
		atking.meme = enemy.meme;
		atking.type = "Meme";
		atking.atk = this.gameObject;
		atking.def = BSM.memeP [Random.Range (0, BSM.memeP.Count)];
		BSM.collectAction (atking);
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
		curState = enemyState.DEAD;
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
			curState = enemyState.WAITING;
		}
	}
}

