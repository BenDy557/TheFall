using UnityEngine;
using System.Collections;

public class EndTrigger : MonoBehaviour {
	public int m_NewItterationCount = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			GameObject LevelGen = GameObject.FindGameObjectWithTag("Level Generator");
			LevelGen.GetComponent<GenerateLevel>().NeedItteration(m_NewItterationCount);
		}
	}
}
