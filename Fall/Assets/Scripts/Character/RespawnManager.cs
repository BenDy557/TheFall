using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour {

	private Camera m_CameraMain;

    public GameObject m_RespawnEffect;

	public float m_RespawnTime = 1.0f;
	// Use this for initialization
	void Start () {
		m_CameraMain = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void NeedRespawn(GameObject Player)
	{
	
		bool RespawnSuceeded = false;

	
			//StartCoroutine(Wait());
			GameObject[] SpawnPointList = GameObject.FindGameObjectsWithTag ("Respawn");
			foreach (GameObject obj in SpawnPointList) 
			{

				Vector3 screenPos = m_CameraMain.WorldToViewportPoint (obj.transform.position);		
				//if (obj.transform.position.x > m_CameraMain.transform.position.x -5 && obj.transform.position.x > m_CameraMain.transform.position.x +5) {
				if (obj.transform.position.y > m_CameraMain.transform.position.y -5 && obj.transform.position.y < m_CameraMain.transform.position.y +5 && !RespawnSuceeded ) 
				{
					// Respawn is on screen
					Player.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
					Player.transform.position = obj.transform.position;

                    Instantiate(m_RespawnEffect, obj.transform.position + new Vector3(0.0f, -1.0f, 0.0f), obj.transform.rotation);

					RespawnSuceeded = true;
					Destroy (obj);
				}
				else 
				{
					if (obj.transform.position.y < m_CameraMain.transform.position.y ) 
					{
						// is lower then camera and is not seen (ie has already passed)
						Destroy (obj);
					}
				}
			}
			if (!RespawnSuceeded) 
			{
			StartCoroutine(Wait(Player));
			}
	}

	public void StartRespawnTimer(GameObject Player)
	{
		StartCoroutine(Wait(Player));
	}

	public IEnumerator Wait(GameObject Player)
	{

		// suspend execution for 5 seconds
		yield return new WaitForSeconds(m_RespawnTime);
		NeedRespawn(Player);
	}
}
