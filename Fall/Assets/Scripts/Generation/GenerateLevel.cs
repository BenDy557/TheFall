using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour {

	public Transform m_StartPoint;
	public GameObject m_BasicBlock;
	float m_NeededItterations = 3;
	public GameObject[] m_LevelPrefabs;
	Transform m_CurrentFocus;
	public GameObject[] m_Pickup;
	public float m_Probability = 0.5f;
	// Use this for initialization
	void Start () {
		SpawnFloor ();
		m_CurrentFocus = m_StartPoint;
		Vector3 pos = m_CurrentFocus.position;
		pos.y += 2;
		m_CurrentFocus.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		while (m_NeededItterations>0) {
			Itterate();
		}
	}

	void SpawnFloor()
	{
		int numberOfItterations = ((10/ (int)m_BasicBlock.transform.localScale.x)+1);

		for (int cnt = -numberOfItterations; cnt < numberOfItterations; cnt++) {
			Vector3 targetLocation = new Vector3(cnt* m_BasicBlock.transform.localScale.x,m_StartPoint.position.y,m_StartPoint.position.z);
			Instantiate(m_BasicBlock,targetLocation,Quaternion.identity);
		}
	}

	public void Itterate()
	{
		GameObject PrefabToSpawn = m_LevelPrefabs [Random.Range (0, m_LevelPrefabs.Length)];
		Vector3 Position = new Vector3 (m_CurrentFocus.transform.position.x, 
		                                m_CurrentFocus.transform.position.y + PrefabToSpawn.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.y/2,
		                                m_CurrentFocus.transform.position.z);
		GameObject SpawnedObject = (GameObject)Instantiate (PrefabToSpawn, Position, Quaternion.identity);

		m_CurrentFocus.position = new Vector3 (m_CurrentFocus.transform.position.x,
		                                       SpawnedObject.GetComponent<PrefabScript> ().UpperLimit.transform.position.y + SpawnedObject.GetComponent<PrefabScript> ().UpperLimit.transform.localScale.y,
		                              m_CurrentFocus.transform.position.z);
		SpawnPickups ();
		m_NeededItterations --;
	}

	public void NeedItteration(int numberNeeded)
	{
		m_NeededItterations+= numberNeeded;
	}

	void SpawnPickups()
	{
		GameObject[] pickupTransforms = GameObject.FindGameObjectsWithTag ("PickupSpawn");
		float tempProbability = Random.Range(0.0f, 1.0f);
		if (tempProbability < m_Probability)
		{
			foreach (GameObject trans in pickupTransforms) {
				Instantiate(m_Pickup[Random.Range (0,m_Pickup.Length)], trans.transform.position, new Quaternion());
				Destroy(trans);
			}
		}

	}
}
