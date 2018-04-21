using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemySM : MonoBehaviour
{
	public Enemy enemy;
	private BattleSM BSM;

	public enum enemyState{
		ACTION,
		WAITING,
		CHOOSE,
		DEAD,
	}

	public enemyState curState;

	public Vector3 ipos;
	private bool action=false;
	public GameObject atktarget;
	private float animSpeed=4f;

	public EnemySM ()
	{
	}

	void Start(){
		curState = enemyState.WAITING;
		BSM = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent <BattleSM>();
		ipos = transform.position;

	}

	void Update(){
		switch (curState) {
		case(enemyState.ACTION):
			{
				StartCoroutine (aksi ());
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
		atking.type = "Musuh";
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
}

