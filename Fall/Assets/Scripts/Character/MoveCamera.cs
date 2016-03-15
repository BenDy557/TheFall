using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	
	public float speed = 3.0f;
	public float m_WaitTime = 5.0f;
	float m_TimeWaited = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// functionality moved to UICountdown script
		//if (m_TimeWaited < m_WaitTime) {
		//	m_TimeWaited += Time.deltaTime;
		//} 
		//else {
			gameObject.transform.Translate (transform.up * speed * Time.deltaTime);
		//}
	}

}
