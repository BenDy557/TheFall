using UnityEngine;
using System.Collections;

//Dummy Prefab class. Any class that requires in game refferences must be accounted for below

public class DummyPrefab : MonoBehaviour {

	public GameObject m_RealPrefab;

	// Use this for initialization
	void Start () {
		if (m_RealPrefab) {

			GameObject NewPrefab = (GameObject)Instantiate(m_RealPrefab,gameObject.transform.position,gameObject.transform.rotation);

			if(gameObject.GetComponent<Rotate>())
			{
				NewPrefab.AddComponent<Rotate>();
				NewPrefab.GetComponent<Rotate>().CopyRotate(gameObject.GetComponent<Rotate>());
			}
			if (gameObject.GetComponent<MovingBlock>())
			{
				//copy component
				NewPrefab.AddComponent<MovingBlock>();
				NewPrefab.GetComponent<MovingBlock>().CopyMovingBlock(gameObject.GetComponent<MovingBlock>());

				//make start object
				GameObject transformStart = new GameObject("Start");
				transformStart.transform.position = gameObject.GetComponent<MovingBlock>().startPos.position;
				Instantiate(transformStart);
				NewPrefab.GetComponent<MovingBlock>().startPos = transformStart.transform;

				//make end object
				GameObject transformEnd = new GameObject("End");
				transformEnd.transform.position = gameObject.GetComponent<MovingBlock>().endPos.position;
				Instantiate(transformEnd);
				NewPrefab.GetComponent<MovingBlock>().endPos = transformEnd.transform;
			}

		} else {
			Debug.Log ("Error: missing prefab resource");
		}

		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
