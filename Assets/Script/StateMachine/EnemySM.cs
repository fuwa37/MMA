using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	private float max_cooldown = 8f;
	public Vector3 ipos;
	private bool action=false;
	public GameObject atktarget;
	private float animSpeed=4f;
	public Text enemyhp;

	public EnemySM ()
	{
	}

	void Start(){
		//texthp = gameObject.transform.GetChild (2).gameObject;
		enemyhp.text = enemy.stat.HP.ToString ();
		Debug.Log (enemyhp);
		curState = enemyState.PROCESSING;
		BSM = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent <BattleSM>();
		ipos = transform.position;

	}

	void Update(){
		enemyhp.text = enemy.stat.HP.ToString ();
		updatehp ();
		switch (curState) {
		case(enemyState.PROCESSING):{
				updatebar ();
				break;
			}
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
				Debug.Log ("DEAD");
				this.transform.gameObject.SetActive (false);
				SceneManager.LoadScene ("MainMenu");
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

		DoDamage ();
		yield return new WaitForSeconds (0.5f);

		Vector3 spos = ipos; 

		while(moveipos(spos)){
			yield return null;
		}

		BSM.action.RemoveAt (0);
		BSM.bs = BattleSM.battleState.WAIT;

		action = false;
		curState = enemyState.PROCESSING;
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
		if (cur_cooldown>max_cooldown){
			curState = enemyState.WAITING;
		}
	}

	void updatehp(){
		float hp = (enemy.stat.HP)/10000f;
		probar.transform.localScale = new Vector3 (Mathf.Clamp (1, 0, hp)
			, probar.transform.localScale.y, probar.transform.localScale.z);
	}

	void DoDamage(){
		float damage = enemy.stat.atkP;
		atktarget.GetComponent <MemeSM>().TakeDamage (damage);
	}

	public void TakeDamage(float amount){
		enemy.stat.HP -= amount;
		if (enemy.stat.HP<=0){
			curState = enemyState.DEAD;
			Debug.Log (curState);
		}
	}


}

