using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public float speed = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate (transform.up * speed * Time.deltaTime);
	}
}
