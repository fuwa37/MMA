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
	private float animSpeed=4f;
	public Text memehp;

	RaycastHit hit; 
	Ray ray; 
	public GameObject target;
		

	public MemeSM ()
	{
	}

	void Start(){
		memehp.text = meme.stat.HP.ToString ();
		curState = charState.PROCESSING;
		Selector.SetActive (false);
		ipos = transform.position;
		BSM = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent <BattleSM>();
	}

	void Update(){
		memehp.text = meme.stat.HP.ToString ();
		Debug.Log(memehp.text)
		updatehp ();
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
				if ((isSelected) && (target)){
					curState = charState.ACTION;
				}
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

		if (Input.GetMouseButtonDown(0)){ 
			ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if (Physics.Raycast (ray, out hit, 100)){ 
				if (hit.transform.tag == "Enemy"){ 
					target = hit.transform.gameObject;
					target.transform.Find ("Targeted").gameObject.SetActive (true);
				} 
			} 
		}
	}

	private IEnumerator aksi(){
		if(action){
			yield break;
		}

		action = true;

		Vector3 targetpos = new Vector3(target.transform.position.x-1.5f,target.transform.position.y,target.transform.position.z);

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
		Selector.SetActive (false);
		isSelected = false;
		target.transform.Find ("Targeted").gameObject.SetActive (false);
		target = null;
		curState = charState.WAITING;
	}

	void selected(){
		TurnHandler atking = new TurnHandler ();
		atking.meme = meme.meme;
		atking.type = "Musuh";
		atking.atk = this.gameObject;
		atking.def = target;
		//int num = 1;
		//atking.choosenAtk = meme.atklist[num];
		//Debug.Log (atking.choosenAtk.atkDmg);
		//Debug.Log (atking.def);
		BSM.collectAction (atking);
	}

	void OnMouseDown(){
		Selector.SetActive (true);
		selected ();
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
		if (cur_cooldown>max_cooldown){
			curState = charState.WAITING;
		}
	}

	void updatehp(){
		float hp = (meme.stat.HP)/10000f;
		probar.transform.localScale = new Vector3 (Mathf.Clamp (1, 0, hp)
			, probar.transform.localScale.y, probar.transform.localScale.z);
	}


	void DoDamage(){
		float damage = meme.stat.atkP;
		target.GetComponent <EnemySM>().TakeDamage (damage);
	}

	public void TakeDamage(float amount){
		meme.stat.HP -= amount;
		if (meme.stat.HP<=0){
			curState = charState.DEAD;
			Debug.Log (curState);
		}
	}
}

