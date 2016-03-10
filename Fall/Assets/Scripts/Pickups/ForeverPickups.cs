using UnityEngine;
using System.Collections;

public class ForeverPickups : MonoBehaviour {

	public GameObject[] m_Pickups;
	GameObject m_ActivePickup;
	bool m_Spawning;
	// Use this for initialization
	void Start () {
		m_ActivePickup = (GameObject)Instantiate ((m_Pickups [Random.Range (0, m_Pickups.Length)]), transform.position, Quaternion.identity);
		m_Spawning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_ActivePickup == null && !m_Spawning ) {
			m_Spawning = true;
			StartCoroutine("Spawn");
		}
	}
	public IEnumerator Spawn()
	{
		
		// suspend execution for 5 seconds
		yield return new WaitForSeconds(5);
		m_ActivePickup = (GameObject)Instantiate ((m_Pickups [Random.Range (0, m_Pickups.Length)]), transform.position, Quaternion.identity);
		m_Spawning = false;
	}
}
