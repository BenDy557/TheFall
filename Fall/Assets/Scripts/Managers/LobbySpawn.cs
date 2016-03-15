using UnityEngine;
using System.Collections;

public class LobbySpawn : MonoBehaviour {

	public Transform[] m_LobbySpawns;
	// Use this for initialization
	void Start () {
		m_LobbySpawns = GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> ().m_LobySpawns;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartRespawnTimer(GameObject Player)
	{
		StartCoroutine(Wait(Player));
	}

	public IEnumerator Wait(GameObject Player)
	{
		
		// suspend execution for 5 seconds
		yield return new WaitForSeconds(2.0f);
		NeedRespawn(Player);
	}

	private void NeedRespawn(GameObject Player)
	{
        Player.GetComponent<Player>().m_IsAlive = true;
		Player.transform.position = m_LobbySpawns[Player.GetComponent<CharacterController>().m_PlayerNumber-1].position;
	}
}
