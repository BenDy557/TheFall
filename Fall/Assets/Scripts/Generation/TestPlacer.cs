using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TestPlacer : MonoBehaviour {

	public GameObject[] LEVELPREFABS;
	public bool TestCustomEntries = false;
	public List<float> EntriesToTest;
	int numberofrows = 0;
	public Transform m_StartPoint;
	Transform m_CurrentFocus;
	float maxheight = 0;
	float maxwidth = 0;
	public float xoffest = 2;
	public float yoffest = 10;
	// Use this for initialization
	void Start () {

		SpawnFloor ();
		m_CurrentFocus = m_StartPoint;
		Vector3 pos = m_CurrentFocus.position;
		pos.y += 2;
		m_CurrentFocus.position = pos;
		foreach (GameObject obj in LEVELPREFABS) {
			if(obj.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.y > maxheight)
			{
				maxheight = obj.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.y;
			}
			if(obj.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.x > maxwidth)
			{
				maxwidth = obj.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.x;
			}
		}


		InstantiateRow (0);
		InstantiateRow (1);
		//InstantiateRow (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstantiateRow(int prefab)
	{
		Vector3 origin = new Vector3 (0, (numberofrows * maxheight *3) + yoffest*numberofrows, 0);
		int cnt = 0;
		GameObject parent = new GameObject ("Row " + prefab);
		parent.transform.position = origin;

		foreach (GameObject obj in LEVELPREFABS) {
			Itterate(prefab, cnt, origin, parent);
			origin.x += maxwidth + xoffest;
			cnt++;
		}
		numberofrows ++;
	}

	public void Itterate(int prefabMid, int prefabEnd, Vector3 startPos, GameObject Par)
	{
		GameObject parent = new GameObject ("" + prefabMid + " X " + prefabEnd);
		parent.transform.position = startPos;
		//Instantiate (parent, startPos, Quaternion.identity);
		//bottome
		GameObject PrefabToSpawn = LEVELPREFABS [prefabEnd];
		Vector3 Position = new Vector3 (startPos.x, 
		                                startPos.y,
		                                startPos.z);

		float newY =  startPos.y + PrefabToSpawn.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.y;

		GameObject SpawnedObject = (GameObject)Instantiate (PrefabToSpawn, Position, Quaternion.identity);
		SpawnedObject.transform.parent = parent.transform;

		//middle
		PrefabToSpawn = LEVELPREFABS [prefabMid];
		Position = new Vector3 (startPos.x, 
		                       			newY,
		                                startPos.z);
		
		newY += PrefabToSpawn.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.y;
		
		SpawnedObject = (GameObject)Instantiate (PrefabToSpawn, Position, Quaternion.identity);
		SpawnedObject.transform.parent = parent.transform;

		// top
		PrefabToSpawn = LEVELPREFABS [prefabEnd];
		Position = new Vector3 (startPos.x, 
		                        newY,
		                        startPos.z);
		
		newY += PrefabToSpawn.GetComponent<PrefabScript> ().BoundingBox.transform.localScale.y;
		
		SpawnedObject = (GameObject)Instantiate (PrefabToSpawn, Position, Quaternion.identity);
		SpawnedObject.transform.parent = parent.transform;
		//Destroy (SpawnedObject, m_DueletePrefabsAfter);
		//m_CurrentFocus.position = new Vector3 (m_CurrentFocus.transform.position.x,
		//                                       SpawnedObject.GetComponent<PrefabScript> ().UpperLimit.transform.position.y + SpawnedObject.GetComponent<PrefabScript> ().UpperLimit.transform.localScale.y,
		 //                                      m_CurrentFocus.transform.position.z);
		//SpawnPickups ();
		//m_NeededItterations --;
		parent.transform.parent = Par.transform;
	}

	void SpawnFloor()
	{
		/*int numberOfItterations = ((10/ (int)m_BasicBlock.transform.localScale.x)+1);

		for (int cnt = -numberOfItterations; cnt < numberOfItterations; cnt++) {
			Vector3 targetLocation = new Vector3(cnt* m_BasicBlock.transform.localScale.x,m_StartPoint.position.y,m_StartPoint.position.z);
			Instantiate(m_BasicBlock,targetLocation,Quaternion.identity);
		}*/
		//Instantiate(m_BasicBlock, m_StartPoint.position, Quaternion.identity);
	}

}
