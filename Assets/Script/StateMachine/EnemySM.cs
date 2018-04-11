using UnityEngine;

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

	public Vector3 pos;

	public EnemySM ()
	{
	}

	void Start(){
		Debug.Log ("aaaaaaa");
		curState = enemyState.WAITING;
		BSM = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent <BattleSM>();
		pos = transform.position;

	}

	void Update(){
		switch (curState) {
		case(enemyState.ACTION):
			{
				break;
			}
		case(enemyState.WAITING):
			{
				waiting ();
				break;
			}
		case(enemyState.CHOOSE):
			{
				choose ();
				curState = enemyState.ACTION;
				break;
			}
		case(enemyState.DEAD):
			{
				break;
			}
		}
	}

	void waiting(){
		curState = enemyState.CHOOSE;
	}

	void choose(){
		TurnHandler atking = new TurnHandler ();
		atking.meme = enemy.meme;
		atking.atk = this.gameObject;
		atking.def = BSM.memeP [Random.Range (0, BSM.memeP.Count)];
		BSM.collectAction (atking);
	}
}

